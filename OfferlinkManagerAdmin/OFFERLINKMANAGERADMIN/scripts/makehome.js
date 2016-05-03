	function frmSubmit(thisPage)
	{
		if (document.all){
			thisPage.style.behavior='url(#default#homepage)';
			thisPage.setHomePage('http://www.snapsearch.com/');
			return false;
		}
		else if (document.getElementById){
			alert("Drag this link onto your Home button to make this your Home Page.");
			return false;
			//document.write('<a href="http://www.YourWebSiteHere.com" onClick="return frmSubmit();">Drag this link onto your Home button to make this your Home Page.</a>');
		}
		else if (document.layers){
		  document.write('<b>Make this site your home page:</b><br>- Go to <b>Preferences</b> in the <B>Edit</B> Menu.<br>- Choose <b>Navigator</b> from the list on the left.<br>- Click on the <b>"Use Current Page"</b> button.');
		}
		else {
		  document.write('<b>Make this site your home page:</b><br>- Go to <b>Preferences</b> in the <B>Edit</B> Menu.<br>- Choose <b>Navigator</b> from the list on the left.<br>- Click on the <b>"Use Current Page"</b> button.');
		}
		
	}

function bookmarkme(){ 
	var txt = "Bookmark Us!";
	var url = window.location.href;
	var who = "Snapsearch.com - The fastest way to search eBay";
	//var who = window.title;

	var ver = navigator.appName;
	var num = parseInt(navigator.appVersion);
	if ((ver == "Microsoft Internet Explorer")&&(num >= 4)) {
		window.external.AddFavorite(url,who);
	}else{
		txt = "Press (Ctrl+D) to Bookmark Us!";
		alert(txt);
	}
}

function setFlag(sid,cur,ctry){
	setOptionIndexOnValue(document.ss.sid,sid);
	setOptionIndexOnValue(document.ss.cur,cur);
	setOptionIndexOnValue(document.ss.ctry,ctry);
	document.ss.submit();	
	return false;
}

function setOptionIndexOnValue(elem,val){
	var cnt = elem.options.length;
	for(i=0;i<cnt;i++){
		if(elem.options[i].value==val){	
			elem.options[i].selected=true;
		}	
	}
}

function setFlagInternet(sid){
	document.ss.sid.value = sid;
	document.ss.submit();
	return false;
}