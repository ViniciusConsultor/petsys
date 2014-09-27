$.extend({
    create: function(type, name) {
        var element = null;

        if (name != null) {
            try {
                element = document.createElement('<' + type + ' name="' + name + '">');
            } catch (e) { }
        }

        if (!element || !element.name) { // Not in IE, then
            element = document.createElement(type)

            if (name)
                element.name = name;
        }

        return $(element);
    }
});