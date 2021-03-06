﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="leftmenu.ascx.cs" Inherits="offerlinkmanageradmin.Controls.leftmenu" %>
 
<style type="text/css">		
		.clear { CLEAR: both }
		#mainContainer { float:left; MARGIN: 0px auto ; WIDTH: 150px; HEIGHT: 100%;  TEXT-ALIGN: left;   }
		#topBar { WIDTH: 760px; HEIGHT: 100px }
		#leftMenu { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FLOAT: left; WIDTH: 180px;  height: 485px; }
		#mainContent { PADDING-RIGHT: 10px; FLOAT: left; WIDTH: 520px ; }
		#dhtmlgoodies_slidedown_menu LI { LIST-STYLE-TYPE: none; POSITION: relative;  }
		#dhtmlgoodies_slidedown_menu UL { PADDING: 0px;  MARGIN: 2px 2px 2px 2px; POSITION: relative }
		#dhtmlgoodies_slidedown_menu DIV { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-TOP: 0px }
		#dhtmlgoodies_slidedown_menu { WIDTH: 180px }
		#dhtmlgoodies_slidedown_menu A { CLEAR: both; DISPLAY: block; /*PADDING-LEFT: 2px; WIDTH: 180px;*/ COLOR: #000; TEXT-DECORATION: none }
		#dhtmlgoodies_slidedown_menu .slMenuItem_depth1 { border-bottom:1px solid #D5D2C3;MARGIN-TOP: 5px; FONT-WEIGHT: bold; }
		#dhtmlgoodies_slidedown_menu .slMenuItem_depth2 { MARGIN-TOP: 5px; /*PADDING-LEFT: 8px*/  background:url(../images/left_submenu.png) no-repeat 0px 3px; padding-left:10px;}
		#dhtmlgoodies_slidedown_menu .slMenuItem_depth3 { MARGIN-TOP: 1px; COLOR: blue; FONT-STYLE: italic }
		#dhtmlgoodies_slidedown_menu .slMenuItem_depth4 { MARGIN-TOP: 1px; COLOR: red }
		#dhtmlgoodies_slidedown_menu .slMenuItem_depth5 { MARGIN-TOP: 1px }
		#dhtmlgoodies_slidedown_menu .slideMenuDiv1 UL { PADDING-RIGHT: 1px; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; PADDING-TOP: 1px }
		#dhtmlgoodies_slidedown_menu .slideMenuDiv2 UL { PADDING-RIGHT: 1px; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; MARGIN-LEFT: 5px; PADDING-TOP: 1px }
		#dhtmlgoodies_slidedown_menu .slideMenuDiv3 UL { PADDING-RIGHT: 1px; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; MARGIN-LEFT: 10px; PADDING-TOP: 1px }
		#dhtmlgoodies_slidedown_menu .slMenuItem_depth4 UL { PADDING-RIGHT: 1px; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; MARGIN-LEFT: 15px; PADDING-TOP: 1px }
</style>

<script type="text/javascript">
    /************************************************************************************************************
    (C) www.dhtmlgoodies.com, October 2005
		
    This is a script from www.dhtmlgoodies.com. You will find this and a lot of other scripts at our website.	
    Terms of use:
    You are free to use this script as long as the copyright message is kept intact. However, you may not
    redistribute, sell or repost it without our permission.
    Update log:
		
    March, 15th: Fixed problem with sliding in MSIE
		
    Thank you!
		
    www.dhtmlgoodies.com
    Alf Magne Kalleland
	
    ************************************************************************************************************/
    var expandFirstItemAutomatically = false; // Expand first menu item automatically ?
    var initMenuIdToExpand = true; // Id of menu item that should be initially expanded. the id is defined in the <li> tag.
    var expandMenuItemByUrl = true; // Menu will automatically expand by url - i.e. if the href of the menu item is in the current location, it will expand
    var initialMenuItemAlwaysExpanded = true; // NOT IMPLEMENTED YET
    var openMenuonDemand;
    var dhtmlgoodies_slmenuObj;
    var divToScroll = false;
    var ulToScroll = false;
    var divCounter = 1;
    var otherDivsToScroll = new Array();
    var divToHide = false;
    var parentDivToHide = new Array();
    var ulToHide = false;
    var offsetOpera = 0;
    if (navigator.userAgent.indexOf('Opera') >= 0) offsetOpera = 1;
    var slideMenuHeightOfCurrentBox = 0;
    var objectsToExpand = new Array();
    var initExpandIndex = 0;
    var alwaysExpanedItems = new Array();

    function popMenusToShow() {
        var obj = divToScroll;
        var endArray = new Array();
        while (obj && obj.tagName != 'BODY') {
            if (obj.tagName == 'DIV' && obj.id.indexOf('slideDiv') >= 0) {
                var objFound = -1;
                for (var no = 0; no < otherDivsToScroll.length; no++) {
                    if (otherDivsToScroll[no] == obj) {
                        objFound = no;
                    }
                }
                if (objFound >= 0) {
                    otherDivsToScroll.splice(objFound, 1);
                }
            }
            obj = obj.parentNode;
        }
    }

    function showSubMenu(e, inputObj) {

        if (this && this.tagName) inputObj = this.parentNode;
        if (inputObj && inputObj.tagName == 'LI') {
            divToScroll = inputObj.getElementsByTagName('DIV')[0];
            for (var no = 0; no < otherDivsToScroll.length; no++) {
                if (otherDivsToScroll[no] == divToScroll) return;
            }
        }

        hidingInProcess = false;
        //alert(otherDivsToScroll.length);
        if (otherDivsToScroll.length > 0) {
            if (divToScroll) {
                if (otherDivsToScroll.length > 0) {
                    popMenusToShow();
                }
                if (otherDivsToScroll.length > 0) {
                    autoHideMenus();
                    hidingInProcess = true;
                }
            }
        }

        if (divToScroll && !hidingInProcess) {
            divToScroll.style.display = '';
            otherDivsToScroll.length = 0;
            otherDivToScroll = divToScroll.parentNode;
            //	alert(divToScroll.innerHTML);
            otherDivsToScroll.push(divToScroll);

            while (otherDivToScroll && otherDivToScroll.tagName != 'BODY') {


                if (otherDivToScroll.tagName == 'DIV' && otherDivToScroll.id.indexOf('slideDiv') >= 0) {
                    otherDivsToScroll.push(otherDivToScroll);

                }
                otherDivToScroll = otherDivToScroll.parentNode;
            }

            ulToScroll = divToScroll.getElementsByTagName('UL')[0];
            if (divToScroll.style.height.replace('px', '') / 1 <= 1) scrollDownSub();
        }
    }


    function autoHideMenus() {
        if (otherDivsToScroll.length > 0) {
            divToHide = otherDivsToScroll[otherDivsToScroll.length - 1];
            //alert(divToHide.parentNode.parentNode.innerHTML);
            parentDivToHide.length = 0;

            var obj = divToHide.parentNode.parentNode.parentNode;

            while (obj && obj.tagName == 'DIV') {

                if (obj.id.indexOf('slideDiv') >= 0) parentDivToHide.push(obj);
                obj = obj.parentNode.parentNode.parentNode;

            }

            var tmpHeight = (divToHide.style.height.replace('px', '') / 1 - slideMenuHeightOfCurrentBox);
            if (tmpHeight < 0) tmpHeight = 0;
            if (slideMenuHeightOfCurrentBox) divToHide.style.height = tmpHeight + 'px';

            ulToHide = divToHide.getElementsByTagName('UL')[0];
            slideMenuHeightOfCurrentBox = ulToHide.offsetHeight;

            scrollUpMenu();

        }
        else {
            slideMenuHeightOfCurrentBox = 0;
            showSubMenu();
        }
    }

    function scrollUpMenu() {
        var height = divToHide.offsetHeight;
        height -= 15;
        if (height < 0) height = 0;
        divToHide.style.height = height + 'px';

        var menuName = divToHide.parentNode.getElementsByTagName('A')[0].innerHTML.split('>');
        //alert(menuName);	

        for (var no = 0; no < parentDivToHide.length; no++) {
            parentDivToHide[no].style.height = parentDivToHide[no].getElementsByTagName('UL')[0].offsetHeight + 'px';
        }

        //	 		if(menuName[1] != 'Email Database')
        //	 		{
        if (height > 0) {
            setTimeout('scrollUpMenu()', 5);
        }
        else {
            divToHide.style.display = 'none';
            otherDivsToScroll.length = otherDivsToScroll.length - 1;
            autoHideMenus();
        }
        //			}
        //			else
        //			{
        //	    	     //	alert ('emaildb');
        //			    otherDivsToScroll.length = otherDivsToScroll.length-1;
        //			    slideMenuHeightOfCurrentBox = 0;
        //			    showSubMenu();	
        //			}
    }

    function scrollDownSub() {

        if (divToScroll) {
            var height = divToScroll.offsetHeight / 1;
            var offsetMove = Math.min(15, (ulToScroll.offsetHeight - height));
            height = height + offsetMove;
            divToScroll.style.height = height + 'px';

            //alert(divToScroll.id);

            for (var no = 1; no < otherDivsToScroll.length; no++) {
                var tmpHeight = otherDivsToScroll[no].offsetHeight / 1 + offsetMove;
                otherDivsToScroll[no].style.height = tmpHeight + 'px';
            }

            if (height < ulToScroll.offsetHeight)
                setTimeout('scrollDownSub()', 5);
            else {
                divToScroll = false;
                ulToScroll = false;
                if (objectsToExpand.length > 0 && initExpandIndex < (objectsToExpand.length - 1)) {
                    initExpandIndex++;
                    //alert(initExpandIndex);
                    showSubMenu(false, objectsToExpand[initExpandIndex]);
                }

            }
        }
    }


    function initSubItems(inputObj, currentDepth) {
        divCounter++;
        var div = document.createElement('DIV'); // Creating new div		
        div.style.overflow = 'hidden';
        div.style.position = 'relative';
        div.style.display = 'none';
        div.style.height = '1px';
        div.id = 'slideDiv' + divCounter;
        div.className = 'slideMenuDiv' + currentDepth;

        inputObj.parentNode.appendChild(div); // Appending DIV as child element of <LI> that is parent of input <UL>		
        div.appendChild(inputObj); // Appending <UL> to the div

        var menuItem = inputObj.getElementsByTagName('LI')[0];

        while (menuItem) {
            if (menuItem.tagName == 'LI') {
                var aTag = menuItem.getElementsByTagName('A')[0];
                aTag.className = 'slMenuItem_depth' + currentDepth;
                var subUl = menuItem.getElementsByTagName('UL');
                if (subUl.length > 0) {
                    initSubItems(subUl[0], currentDepth + 1);
                }
                aTag.onclick = showSubMenu;
            }
            menuItem = menuItem.nextSibling;
        }
    }

    function initSlideDownMenu() {

        dhtmlgoodies_slmenuObj = document.getElementById('dhtmlgoodies_slidedown_menu');

        dhtmlgoodies_slmenuObj.style.visibility = 'visible';
        var mainUl = dhtmlgoodies_slmenuObj.getElementsByTagName('UL')[0];
        var mainMenuItem = mainUl.getElementsByTagName('LI')[0];
        mainItemCounter = 1;
        while (mainMenuItem) {

            if (mainMenuItem.tagName == 'LI') {
                var aTag = mainMenuItem.getElementsByTagName('A')[0];
                var menuname = aTag.innerHTML.split('>');

                //					if (menuname[1] == 'Email Database')
                //					{
                //					    aTag.className='slMenuItem_depth1';	
                //					    var subUl = mainMenuItem.getElementsByTagName('UL');
                //					    if(subUl.length>0){
                //						    mainMenuItem.id = 'mainMenuItem1000';
                //						    initSubItems(subUl[0],2);
                //						    aTag.onclick = showSubMenu;
                //						    mainItemCounter++;
                //					    }
                //					} 
                //					else
                //					{

                aTag.className = 'slMenuItem_depth1';
                var subUl = mainMenuItem.getElementsByTagName('UL');
                if (subUl.length > 0) {
                    mainMenuItem.id = 'mainMenuItem' + mainItemCounter;
                    initSubItems(subUl[0], 2);
                    aTag.onclick = showSubMenu;
                    mainItemCounter++;
                }

                //					}	

                //}			
            }
            mainMenuItem = mainMenuItem.nextSibling;
        }

        if (location.search.indexOf('mainMenuItemToSlide') >= 0) {
            var items = location.search.split('&');
            for (var no = 0; no < items.length; no++) {
                if (items[no].indexOf('mainMenuItemToSlide') >= 0) {
                    values = items[no].split('=');
                    showSubMenu(false, document.getElementById('mainMenuItem' + values[1]));
                    initMenuIdToExpand = false;
                }
            }
        } else if (expandFirstItemAutomatically > 0) {
            if (document.getElementById('mainMenuItem' + expandFirstItemAutomatically)) {
                showSubMenu(false, document.getElementById('mainMenuItem' + expandFirstItemAutomatically));
                initMenuIdToExpand = false;
            }
        }


        if (expandMenuItemByUrl) {
            var aTags = dhtmlgoodies_slmenuObj.getElementsByTagName('A');
            for (var no = 0; no < aTags.length; no++) {
                var hrefToCheckOn = aTags[no].href;

                //alert(hrefToCheckOn);

                if (location.href.indexOf(hrefToCheckOn) >= 0 && hrefToCheckOn.indexOf('#') < hrefToCheckOn.length - 1) {
                    //	initMenuIdToExpand = false;


                    var obj = aTags[no].parentNode;
                    while (obj && obj.id != 'dhtmlgoodies_slidedown_menu') {

                        // alert(alwaysExpanedItems[obj.parentNode]);

                        if (obj.tagName == 'LI') {
                            var subUl = obj.getElementsByTagName('UL');

                            if (initialMenuItemAlwaysExpanded)
                                alwaysExpanedItems[obj.parentNode] = true;

                            if (subUl.length > 0) {

                                objectsToExpand.push(obj);

                            }

                        }
                        obj = obj.parentNode;
                    }

                    //	alert(" total : " + objectsToExpand.length);
                    //				        if(objectsToExpand.length > 1)
                    //				            showSubMenu(false,objectsToExpand[1]);
                    //				        else
                    showSubMenu(false, objectsToExpand[0]);

                    break;
                }

            }

        }


        if (!initMenuIdToExpand) {

            var obj = document.getElementById('mainMenuItem1000')

            while (obj && obj.id != 'dhtmlgoodies_slidedown_menu_email') {
                if (obj.tagName == 'LI') {
                    var subUl = obj.getElementsByTagName('UL');

                    //alert(obj.parentNode.innerHTML);

                    if (initialMenuItemAlwaysExpanded) alwaysExpanedItems[obj.parentNode] = true;
                    if (subUl.length > 0) {
                        //alert(obj.innerHTML);					
                        objectsToExpand.unshift(obj);
                    }
                }
                obj = obj.parentNode;
            }


            showSubMenu(false, objectsToExpand[0]);

            //setTimeout('test()',5);
        }
        openMenuonDemand = <%=exitclickid %>;
        if (openMenuonDemand > 0) {
            //alert('demandOpen');
            var obj = document.getElementById('mainMenuItem' + openMenuonDemand)

            while (obj && obj.id != 'dhtmlgoodies_slidedown_menu_email') {
                if (obj.tagName == 'LI') {
                    var subUl = obj.getElementsByTagName('UL');

                    //alert(obj.parentNode.innerHTML);

                    if (initialMenuItemAlwaysExpanded) alwaysExpanedItems[obj.parentNode] = true;
                    if (subUl.length > 0) {
                        //alert(obj.innerHTML);					
                        objectsToExpand.unshift(obj);
                    }
                }
                obj = obj.parentNode;
            }


            showSubMenu(false, objectsToExpand[0]);

            //setTimeout('test()',5);
        }

    }


    window.onload = initSlideDownMenu;
			 
</script>

 
<!-- START OF MENU -->
<div id="dhtmlgoodies_slidedown_menu">
    <ul>        
        
        <li id="tracking"><a href="#" class="slMenuItem_depth1">
            <img border="0" height="14" hspace="1" src="<%=BLL.Constants.AdminURL %>images/blog_arrow.jpg"
                width="11">Tracking</a>
            <ul>
                <li><a class="slMenuItem_depth2" href="<%=BLL.Constants.AdminURL %>OfferLink/ListOfferLinks.aspx"
                    title="View Promotional Link List">Promotional Link List</a>
                 </li>
                 <li><a class="slMenuItem_depth2" href="<%=BLL.Constants.AdminURL %>OfferLink/AddEditOfferLink.aspx"
                    title="View Add Edit Promotional Link">Add/Edit Promotional Link</a>
                 </li> 
                                 
                 
                 <li><a class="slMenuItem_depth2" href="<%=BLL.Constants.AdminURL %>Report/List_ExitClickOfferLinkReport.aspx"
                    title="View Exit Click By Promotiona Link">Link Wise Click</a>
                 </li> 
                                       
            </ul>
        </li>       
          
                
    </ul>
  
</div>

<div>
    <ul>
        <asp:Literal ID="ltleftmenuEmailDatabase" runat="server"></asp:Literal>
    </ul>
</div>

<!-- END OF MENU -->
<div class="clear">
</div>
