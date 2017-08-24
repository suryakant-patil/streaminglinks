function show_details(id)
{	
	window.parent.parent.frames["fraRightFrame"].location.href="Category/CategoriesFrame.aspx?cid="+id ;
	alert(id);
}

function show_subdetails(id)
{
	window.parent.parent.parent.frames["fraRightFrame"].location.href="../Keyword/KeywordFrame.aspx?sid="+id;
	
}
function show_maincatdetails(lid)
{
	window.parent.parent.frames["fraRightFrame"].location.href="Category/MainCategoriesFrame.aspx?lid="+lid ;
}

function addProjectNode(pid){	
	if(pid>0){
		var telem = document.getElementById("tr_pr_"+pid);
		if(!telem){
			var elem = document.getElementById("myprojects");
			if(elem){
				elem = elem.parentElement.parentElement.parentElement.nextSibling;
				var tmp  = elem.firstChild;	
				var oRow = document.createElement("TR");
				oRow.setAttribute("id","tr_pr_"+pid);
				tmp.appendChild(oRow);
				oCell = document.createElement("TD");
				oCell.innerHTML = "<IMG class=ob_ic src=images/tree/treeStyle_msdn/n.gif>";
				oRow.appendChild(oCell);
				oCell = document.createElement("TD");
				var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
				var ran_number= Math.random()*4;		
				xmlhttp.open("GET", "Projects/getNewProjectNode.aspx?rand="+ran_number, false);
				xmlhttp.send("");
				oCell.innerHTML = xmlhttp.responseText;
				oRow.appendChild(oCell);		
			}else{
			}
		}	
	}
}

function renameProjectNode(pid,newName,statChar){
	if(pid>0){
		var telem = document.getElementById("prj_txt_"+pid);
		if(telem){
			telem.innerText=newName;
		}
		toggleProjectImage(pid,statChar);		
	}
}

function toggleProjectStatus(pid,statChar){
	if(pid>0){
		var xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
		var ran_number= Math.random()*4;		
		xmlhttp.open("GET", "Projects/updateProjectStatus.aspx?pid="+ pid + "&stat="+ statChar +"&tme="+ran_number , false);		
		xmlhttp.send("");		
		var result = xmlhttp.responseText;
		//alert(result);
		if(result=="N" || result=="Y"){
			toggleProjectImage(pid,statChar);
		}else{	
			// ignore 		
		}
	}
}

function toggleProjectImage(pid,statChar){
	var telem = document.getElementById("prj_img_"+pid);		
	if(telem){
		if(statChar=="Y"){
			telem.src="images/project_enabled.gif";
		}else{
			telem.src="images/project_disabled.gif";
		}
	}	
}

function show_webalerts(){	
	var rand = Math.random()*10;
	window.parent.parent.frames["fraRightFrame"].location.href="Reports/Webalerts/ReportsFrame.aspx?rand="+rand;
}
function show_feedbacks(){
	var rand = Math.random()*10;
	window.parent.parent.frames["fraRightFrame"].location.href="Reports/Feedback/ReportsFrame.aspx?rand="+rand;
}

function show_searchterms(){
	var rand = Math.random()*10;
	window.parent.parent.frames["fraRightFrame"].location.href="Reports/Searchterms/ReportsFrame.aspx?rand="+rand;
}

