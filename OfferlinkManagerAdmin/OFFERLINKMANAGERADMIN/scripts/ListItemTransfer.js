// JScript File  for transfering list item from left to right and right to left

	//select tags	        
     function movelistListbox(Action,ltId1,ltId2)
		{
		  
			var lstFrom =document.getElementById(ltId1);
			var lstTo =document.getElementById(ltId2);
			 
		if(Action=="right")
		{
		//check user select value or not
		if(lstFrom.selectedIndex ==-1)
		{
			alert('Please select atleast one record to Add From Left Side');
			return;
		}
		else
		
		{
		//shift multiple values
		while(lstFrom.selectedIndex >=0)
		{  
		var selIndex =lstFrom.selectedIndex;
		var optionText =lstFrom.options[selIndex].text;
		var optionVal =lstFrom.options[selIndex].value;
		var optionIndex =lstTo.options.length;
		var objOption = new Option(optionText,optionVal);
		lstTo.options[optionIndex] =objOption;
		lstFrom.options[selIndex] = null;
		}
		
		}
		
		
		}
		else
		{
			
		if(lstTo.selectedIndex ==-1)
		{
			alert('Please select atleast one record to Add From Right Side');
			return;
		}
		else
		
		{
		while(lstTo.selectedIndex >=0)
		{ 
		var selIndex =lstTo.selectedIndex;
		var optionText =lstTo.options[selIndex].text;
		var optionVal =lstTo.options[selIndex].value;
		var optionIndex =lstFrom.options.length;
		var objOption = new Option(optionText,optionVal);
		lstFrom.options[optionIndex] =objOption;
		lstTo.options[selIndex] = null;
		}
		}
		
		}
}