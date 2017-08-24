	var d = document;
	if(d.layers) {
		var isNav = 1;
		var isIE = 0; isNS6 = 0;
		}
	else if(d.all) {
		var isIE = 1;
		var isNav = 0, isNS6 = 0;
		}
	else if (d.getElementByID) {
		var isNS6 = 1;
		var isNav = 0, isIE = 0;
		}

	function hideMe(thing,src) {	
		var elem = document.getElementById(thing);	
		if(elem){
			elem.style.display="none";
			var elem = document.getElementById('cpanel');	
			if(elem){
				elem.src= src;
			}
		}
	}
	
	function pp(){
		alert('pp');
	}
	function showMe(thing,src,caption,width,height) {	
		
		var elem = document.getElementById('cpanel');	
		if(elem){	
			var ran_number= Math.random()*4;		
			elem.src= src + "&rand="+ ran_number;			
			//elem.contentWindow.document.location.href=src;			
		}
		
		var elem = document.getElementById('caption');	
		if(elem){
			elem.innerText= caption;			
			//elem.contentWindow.document.location.reload();
		}
		
		var elem = document.getElementById(thing);	
		if(elem){			
			elem.style.left = "100px";
			elem.style.top = "30px";
			elem.style.pixelWidth = width;
			elem.style.pixelHeight = height;
			elem.style.display="block";	
		}
	}
	
	function shiftTo(obj, x, y) {
		if(isIE) {
			theObj = eval("d.all."+obj+".style");
			theObj.pixelLeft = x;
			theObj.pixelTop = y;
			}
		else if(isNav) {
			theObj = eval("d."+obj);
			theObj.moveTo(x,y);
			}
		else {
			theObj = eval("d.getElementById('"+obj+"')");
			theObj.style.top = y;
			theObj.style.left = x;
			}
		}
	// END STANDARD GLEN COMPONENTS

	// START DRAGGABLE SPECIFIC CODE
	var offsetX, offsetY, curz = 1;
	var mx, my, activeelement;

	function engage(thing) {
		activeelement = thing;
		if(isIE) {
			offsetX = window.event.offsetX + 2;
			offsetY = window.event.offsetY + 2;
			theObj = eval("d.all."+thing+".style");
			}
		else if(isNav) {
			eval("offsetX = mousex - document."+activeelement+".left");
			eval("offsetY = mousey - document."+activeelement+".top");
			theObj = eval("d."+thing);
			}
		else {
			eval("offsetX = mousex - parseInt(d.getElementById('"+activeelement+"').style.left)");
			eval("offsetY = mousey - parseInt(d.getElementById('"+activeelement+"').style.top)");
			theObj = eval("d.getElementById('"+activeelement+"').style");
			}
		theObj.zIndex = curz++;
		}

	function release() {
		activeelement = null;
		}

	function mousemoved(evt) {
			if(isIE) {
				mousex = window.event.clientX+document.body.scrollLeft;
				mousey = window.event.clientY+document.body.scrollTop;
				}
			else if(isNav) {
				mousex = evt.pageX+window.pageXOffset;
				mousey = evt.pageY+window.pageYOffset;
				}
			else {
				mousex = evt.pageX;
				mousey = evt.pageY;
				}
    	if(activeelement) {
    	  wx = mousex - offsetX;
   			wy = mousey - offsetY;
   			wx = (wx>0) ? wx:0;
   			wy = (wy>0) ? wy:0;
    		shiftTo(activeelement,wx, wy);
				}
			return false;
			}

	if(isNav) {
 		window.captureEvents(Event.MOUSEMOVE);
 		window.onmousemove = mousemoved;
		}
	else {
		document.onmousemove = mousemoved;
		}
	// END DRAGGABLE CODE