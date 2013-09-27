/*
This file is part of Ext JS 3.4

Copyright (c) 2011-2013 Sencha Inc

Contact:  http://www.sencha.com/contact

GNU General Public License Usage
This file may be used under the terms of the GNU General Public License version 3.0 as
published by the Free Software Foundation and appearing in the file LICENSE included in the
packaging of this file.

Please review the following information to ensure the GNU General Public License version 3.0
requirements will be met: http://www.gnu.org/copyleft/gpl.html.

If you are unsure which license is appropriate for your use, please contact the sales department
at http://www.sencha.com/contact.

Build date: 2013-04-03 15:07:25
*/
Ext.Desktop = function (app) {
    this.taskbar = new Ext.ux.TaskBar(app);
    this.xTickSize = this.yTickSize = 1;
    var taskbar = this.taskbar;

    var desktopEl = Ext.get('x-desktop');
    var taskbarEl = Ext.get('ux-taskbar');
    var shortcuts = Ext.get('shortcuts');

    var windows = new Ext.WindowGroup();
    var activeWindow;

    function minimizeWin(win) {
        win.minimized = true;
        win.hide();
    }

    function markActive(win) {
        if (activeWindow && activeWindow != win) {
            markInactive(activeWindow);
        }
        taskbar.setActiveButton(win.taskButton);
        activeWindow = win;
        Ext.fly(win.taskButton.el).addClass('active-win');
        win.minimized = false;
    }

    function markInactive(win) {
        if (win == activeWindow) {
            activeWindow = null;
            Ext.fly(win.taskButton.el).removeClass('active-win');
        }
    }

    function removeWin(win) {
        taskbar.removeTaskButton(win.taskButton);
        layout();
    }

    function layout() {
        desktopEl.setHeight(Ext.lib.Dom.getViewHeight() - taskbarEl.getHeight());
    }
    Ext.EventManager.onWindowResize(layout);

    this.layout = layout;

    this.createWindow = function (config, cls) {
        var win = new (cls || Ext.Window)(
        Ext.applyIf(config || {},
        {
            renderTo: desktopEl,
            manager: windows,
            minimizable: true,
            maximizable: true
        })
        );
        win.dd.xTickSize = this.xTickSize;
        win.dd.yTickSize = this.yTickSize;
        if (win.resizer) {
            win.resizer.widthIncrement = this.xTickSize;
            win.resizer.heightIncrement = this.yTickSize;
        }
        win.render(desktopEl);
        win.taskButton = taskbar.addTaskButton(win);

        win.cmenu = new Ext.menu.Menu({
            items: [

            ]
        });

        win.animateTarget = win.taskButton.el;

        win.on({
            'activate': {
                fn: markActive
            },
            'beforeshow': {
                fn: markActive
            },
            'deactivate': {
                fn: markInactive
            },
            'minimize': {
                fn: minimizeWin
            },
            'close': {
                fn: removeWin
            }
        });

        layout();
        return win;
    };

    this.getManager = function () {
        return windows;
    };

    this.getWindow = function (id) {
        return windows.get(id);
    };

    this.getWinWidth = function () {
        var width = Ext.lib.Dom.getViewWidth();
        return width < 200 ? 200 : width;
    };

    this.getWinHeight = function () {
        var height = (Ext.lib.Dom.getViewHeight() - taskbarEl.getHeight());
        return height < 100 ? 100 : height;
    };

    this.getWinX = function (width) {
        return (Ext.lib.Dom.getViewWidth() - width) / 2;
    };

    this.getWinY = function (height) {
        return (Ext.lib.Dom.getViewHeight() - taskbarEl.getHeight() - height) / 2;
    };

    this.setTickSize = function (xTickSize, yTickSize) {
        this.xTickSize = xTickSize;
        if (arguments.length == 1) {
            this.yTickSize = xTickSize;
        } else {
            this.yTickSize = yTickSize;
        }
        windows.each(function (win) {
            win.dd.xTickSize = this.xTickSize;
            win.dd.yTickSize = this.yTickSize;
            win.resizer.widthIncrement = this.xTickSize;
            win.resizer.heightIncrement = this.yTickSize;
        },
        this);
    };

    this.cascade = function () {
        var x = 0,
        y = 0;
        windows.each(function (win) {
            if (win.isVisible() && !win.maximized) {
                win.setPosition(x, y);
                x += 20;
                y += 20;
            }
        },
        this);
    };

    this.tile = function () {
        var availWidth = desktopEl.getWidth(true);
        var x = this.xTickSize;
        var y = this.yTickSize;
        var nextY = y;
        windows.each(function (win) {
            if (win.isVisible() && !win.maximized) {
                var w = win.el.getWidth();

                //              Wrap to next row if we are not at the line start and this Window will go off the end
                if ((x > this.xTickSize) && (x + w > availWidth)) {
                    x = this.xTickSize;
                    y = nextY;
                }

                win.setPosition(x, y);
                x += w + this.xTickSize;
                nextY = Math.max(nextY, y + win.el.getHeight() + this.yTickSize);
            }
        },
        this);
    };

    this.contextMenu = new Ext.menu.Menu({
        items: [{
            text: 'Titulo',
            handler: this.tile,
            scope: this
        },
        {
            text: 'Cascata',
            handler: this.cascade,
            scope: this
        }]
    });
    desktopEl.on('contextmenu',
        function (e) {
            e.stopEvent();
            this.contextMenu.showAt(e.getXY());
        },
        this);

    layout();

    if (shortcuts) {
        shortcuts.on('click', function (e, t) {
            if (t = e.getTarget('dt', shortcuts)) {
                e.stopEvent();
                var desktop = app.getDesktop();
                var htm = '<div id="div_' + t.id + '" style="position: absolute; width: 100%; height: 100%; background-color: white; padding: 0pt;"> <div style="position: absolute; text-align: center; width: 100%; height: 70px; top: 40%;"> <img src="imagens/carregandopagina.gif" alt=""></div></div>  <iframe src="' + t.firstChild.href + '" style="width: 100%; height: 100%; background-color: white;" onload="hideLoading(div_' + t.id + ')"></iframe>';

                if (t.id.indexOf('externo') != -1)
                    htm =  '<iframe src="' + t.firstChild.href + '" style="width: 100%; height: 100%; background-color: white;"></iframe>'                              

                var win = desktop.getWindow('win' + t.id);
                if (!win) {


                    win = desktop.createWindow({
                        id: 'win' + t.id,
                        title: t.innerText,
                        width: 800,
                        height: 550,
                        html: htm,
                        iconCls: 'bogus',
                        shim: false,
                        animCollapse: false,
                        constrainHeader: true

                    });
                }
                win.show();
            }
        });
    }
};
