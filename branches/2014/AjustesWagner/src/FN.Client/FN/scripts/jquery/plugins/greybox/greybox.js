/* Greybox Redux
 * Required: http://jquery.com/
 * Written by: John Resig
 * Based on code by: 4mir Salihefendic (http://amix.dk)
 * License: LGPL (read more in LGPL.txt)
 */

var GB_DONE = false;
var GB_HEIGHT = 400;
var GB_WIDTH = 400;
var FRAME = "";

var caminhoParaGreyBox = 'scripts/jquery/plugins/greybox/';

String.prototype.trim = function()
{
return this.replace(/^\s+|\s+$/g, "");
}

function GB_show(root, caption, url, height, width,fecha) {
  try {
      GB_HEIGHT = height || 100;
      GB_WIDTH = width || 100;
      if(!GB_DONE) {
        
            FRAME = "<IFrame id='GB_overlay' frameborder=0 style='background-color: #000; opacity:0.8; -moz-opacity: 0.8; filter: alpha(opacity=80)'></IFrame><div id='GB_window'>";

		    if (caption.trim() != "")
			    FRAME += "<div id='GB_caption'></div>";
    		
           if (fecha == "True") { 
                FRAME += "<img src='" + root + "/" + caminhoParaGreyBox + "fechar.GIF' alt='Fechar' id='btnFecharPoupUp'/></div>";
                $(document.body).append(FRAME);
                $("#GB_window img").click(GB_hide);
           } else { 
                  $(document.body).append(FRAME +  "</div>");
           }
        
        $(window).load(GB_position);
        $(window).resize(GB_position);
        $(window).scroll(GB_position);

        $('select').hide();
        
        GB_DONE = true;
      }

      $("#GB_frame").remove();
      $("#GB_window").append("<iframe id='GB_frame' src='" + root + "/" + url + "'></iframe>");
      
      GB_defineStyles();

      if (caption.trim() != "")
        $("#GB_caption").html(caption);

      $("#GB_overlay").show();
      
      GB_position();
      
      if(GB_ANIMATION)
        $("#GB_window").slideDown('slow');
      else
        $("#GB_window").show();
     } catch(e) {
        alert('oops on show: '+e);
     }
}

function GB_defineStyles() {
      $("#GB_window").css({
          "position": "absolute",
          "background": "#fff",
          "border-right": "3px solid #aaa",
          "border-bottom": "3px solid #aaa",
          "overflow": "auto",
          "width": "400px",
          "height": "400px",
          "z-index": "150"
      });

      $('#GB_overlay').css({
          "background-image": "url(scripts/jquery/plugins/greybox/overlay.png)",
          "position": "absolute",
          "margin": "auto",
          "top": "0",
          "left": "0",
          "z-index": "100"
      });

      $('#GB_frame').css({
          "border": "0",
          "overflow": "auto",
          "width": "100%",
          "height": "378px"      
      });
      
      $('#GB_caption').css({
          "font": "12px bold helvetica, verdana, sans-serif",
          "color": "#fff",
          "background": "#888888",
          "padding": "2px 0 2px 5px",
          "margin": "0",
          "text-align": "left"
      });
      
      $('#GB_window img').css({
          "position": "absolute",
          "top": "2px",
          "right": "5px",
          "cursor": "pointer",
          "cursor": "hand"
      });
}

function GB_hide() {
  $("#GB_window,#GB_overlay").hide();
    $('select').show();
}

function GB_close() {
    $('select').show();
    $("#GB_window img").click();
}

function GB_position() {
    try {
        var scrolledX, scrolledY;
        if( self.pageYOffset ) {
            scrolledX = self.pageXOffset;
            scrolledY = self.pageYOffset;
        } else if( document.body && document.body.parentNode ) {
            scrolledX = document.body.parentNode.scrollLeft;
            scrolledY = document.body.parentNode.scrollTop;
        } else if( document.body ) {
            scrolledX = document.body.scrollLeft;
            scrolledY = document.body.scrollTop;
        }
        
        //alert("scrolled " + scrolledX + ", "+scrolledY);

        var centerX, centerY;
        if( self.innerHeight ) {
            centerX = self.innerWidth;
            centerY = self.innerHeight;
        } else if( document.documentElement && document.documentElement.clientHeight ) {
            centerX = document.documentElement.clientWidth;
            centerY = document.documentElement.clientHeight;
        } else if( document.body ) {
            centerX = document.body.clientWidth;
            centerY = document.body.clientHeight;
        } 

        //alert("center " + centerX + ", "+centerY);

        var leftOffset = scrolledX + (centerX - GB_WIDTH) / 2;
        var topOffset = scrolledY + (centerY - GB_HEIGHT) / 2; 

        //alert("offset" + leftOffset + ", "+topOffset + "(Max)" + Math.max(0, leftOffset) + ", " + Math.max(0, topOffset));

		$("#GB_window").css({
			width:GB_WIDTH+"px",
			height:GB_HEIGHT+"px",
			left: Math.max(0, leftOffset)+"px",
			top:  Math.max(0, topOffset)+"px"
		});
		
		$("#GB_frame").css("height",GB_HEIGHT - 32 +"px");

        var windowwidth = Math.max(
  			    document.body.parentNode.scrollLeft + document.body.scrollWidth,
  			    document.body.clientWidth);
        var windowheight = Math.max(
  			    document.body.parentNode.scrollTop + document.body.scrollHeight,
  			    document.body.clientHeight);

        $("#GB_overlay").css({"z-index":"2", position:"absolute", float:"left", top:"0px", left:"0px", height:windowheight+"px",width:"110%" });
    } catch(e) {
        alert('oops on position: '+e);
    }

}
