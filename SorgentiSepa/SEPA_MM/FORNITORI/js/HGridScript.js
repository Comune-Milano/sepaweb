
function getElement(aID)
{ 
     return (document.getElementById) ? document.getElementById(aID) : getElement[aID];
}

//This method will switch the plus to a minus and vice versa on the client side
function ChangePlusMinusText(PlusMinusCellID)
{
	try
	{
		
		var PlusMinusCellObj = getElement(PlusMinusCellID);
		if ( PlusMinusCellObj != null )
		{
			if ( PlusMinusCellObj.innerHTML == "<A>+</A>")
			{
				PlusMinusCellObj.innerHTML = "<A>-</A>";
				
			}
			else if ( PlusMinusCellObj.innerHTML == "<a>+</a>")
			{
				PlusMinusCellObj.innerHTML = "<A>-</A>";
				
			}
			else
			{
				PlusMinusCellObj.innerHTML = "<A>+</A>";
			}
		}
	}
	catch(e)
	{ 
		alert( "Error in ChangePlusMinusText Method: " + e); 
	}
}

//After a postback occurs, this method will re-expand old data.
function ShowExpandedDivInfo(TextBoxIDOfExpandedDiv, DataGridID)
{
	try
	{
		var DIVTB = getElement(TextBoxIDOfExpandedDiv);
		if ( DIVTB != null )
		{	
			var data = DIVTB.value;
			if ( data != null && data.length > 0 && data != ",")
			{
				var SplitData = data.split(",");
				
				//Temp variables
				var SplitDataOfArrayItem = "";
				var DivIdentifier = "";
				var PlusMinusCellIdentifier = "";
				
				for ( var i = 0; i < SplitData.length; i++)
				{
					//Format of textbox data
					//NameOfDIV + "@" + PlusMinusCellID
					SplitDataOfArrayItem = SplitData[i].split("@");
					//Get the ID of the DIV section
					DivIdentifier = SplitDataOfArrayItem[0];
					//Get the ID of the Cell with the Plus or Minus showing collapsed or expanded state.
					PlusMinusCellIdentifier = SplitDataOfArrayItem[1];
					
					if ( DivIdentifier != null && DivIdentifier.length > 0 )
					{
						//Expand DIV Section
						ShowPanel( DivIdentifier );
					}
					
					if ( PlusMinusCellIdentifier != null && PlusMinusCellIdentifier.length > 0 )
					{
						var PlusMinusCellObj = getElement(PlusMinusCellIdentifier);
						if ( PlusMinusCellObj != null )
						{
							//We found the cell to expand
							PlusMinusCellObj.innerHTML = "<A>-</A>";
						}
						else
						{
							//We DID NOT FIND the cell to expand
							//May Have Been Lost After Postback.
							
							//Invoke Method to get id of cell using partial data which will always
							//be unique...
							var NewPlusMinusCellObjID = getItem(PlusMinusCellIdentifier, DataGridID);
							
							if ( NewPlusMinusCellObjID != null )
							{
								var NewCellObject = getElement(NewPlusMinusCellObjID);
								if ( NewCellObject != null )
								{
									var rows = NewCellObject.getElementsByTagName("td");   
									if ( rows != null && rows.length >= 0)
									{
										//the initial row has to be set to expanded....
										try
										{
											rows[0].innerHTML = "<A>-</A>";
										}
										catch(e)
										{
											//alert("Error in setting row to expanded in ShowExpandedDivInfo Method.  Error 101: " + e);
										}
										
									}//end of rows if statement
								}//end of NewCellObject != null
							}//end of NewPlusMinusCellObjID != null
						}//end of PlusMinusCellObj != null if else
					}//end of PlusMinusCellIdentifier != null
				}//end of for loop
			}
		}
	}
	catch(e)
	{
		alert( "Error in ShowExpandedDivInfo Method: " + e);
	}
}

//This will keep the data in the hidden textbox field.
function SetExpandedDIVInfo(PlusMinusCellID, HiddenTextBoxToHoldDivInfo, DIVInfoID)
{
	try
	{
		var IsExpanded = false;
		var HTBObj = getElement(HiddenTextBoxToHoldDivInfo);
		var HLObj = getElement(PlusMinusCellID);
		
		if ( HLObj != null && HTBObj != null)
		{
		
			if ( HLObj.innerHTML == "<A>+</A>")
			{
				IsExpanded = false;	
			}
			else
			{
				IsExpanded = true;
			}
					
			//What is in the hidden DIV Textbox field?
			var ExpandedData = HTBObj.value;
			
			if ( ExpandedData == null || ExpandedData == "," )
			{
				ExpandedData = "";
			}
			
			//===============================================================
			if ( ExpandedData.length < 1 && IsExpanded == true )
			{
				//No Previous Expanded Data.  
				//Add new Expanded Field.
				ExpandedData = DIVInfoID + "@" + PlusMinusCellID;
				HTBObj.value = ExpandedData;
			}
			else if ( ExpandedData.length < 1 && IsExpanded == false )
			{
				//No Previous Expanded Data.  
				//Clean up old Expanded Field. 
				//ExpandedData is empty, so no work is needed.
			}
			
			else if ( ExpandedData.length > 0 && ExpandedData.indexOf(DIVInfoID) == -1 && IsExpanded == true )
			{
				//Expanded data has data from before.
				//No existing record exists for this field. 
				//We can add new Expanded field. 
				//We can use a comma as a delimeter.
				ExpandedData = ExpandedData + "," + DIVInfoID + "@" + PlusMinusCellID;
				ExpandedData = ExpandedData.replace( ",," , ",");
				HTBObj.value = ExpandedData;
			}
			else if ( ExpandedData.indexOf(DIVInfoID) > -1 && IsExpanded == true )
			{
				//Expanded data has data from before. 
				//Existing record exists for this field. 
				//We do not need to perform any updates.
			}
			else if ( ExpandedData.indexOf(DIVInfoID) > -1 && IsExpanded == false )
			{
				//Expanded data has data from before.
				//Existing record exists for this field
				//We remove it as it is not expanded any longer.
				ExpandedData = ExpandedData.replace(DIVInfoID  + "@" + PlusMinusCellID ,"");
				
				//Make sure we don't have a double delimeter
				ExpandedData = ExpandedData.replace( ",," , ",");
				HTBObj.value = ExpandedData;
			}
			else if ( ExpandedData.indexOf(DIVInfoID) == -1 && IsExpanded == false )
			{
				//Expanded data has data from before.
				//Existing record does not exists for this field
				//No work is needed.
			}
			
			//Recheck we don't have any garbage data.
			//Added because deleting items, causes a comma
			//to stay sometimes.
			if ( HTBObj != null && HTBObj.value == "," )
			{
				HTBObj.value = "";
			}
			//===============================================================
		}	
	}
	catch(e)
	{ 
		alert( "Error in SetExpandedDIVInfo Method: " + e); 
	}
}

//This method will hide the panel.
function HidePanel(Panel)
{
	try
	{
		var ChosenPanel = getElement(Panel);
		if ( ChosenPanel != null )
		{
			getElement(Panel).style.display = "none";
		}
	}
	catch(e)
	{
		alert( "Error in HidePanel Method: " + e); 
	}
}

//This method will show the panel.	
function ShowPanel(Panel)
{
	try
	{
		var ChosenPanel = getElement(Panel);
		if ( ChosenPanel != null )
		{
			getElement(Panel).style.display = "block";
		}
	}
	catch(e)
	{
		alert( "Error in ShowPanel Method: " + e); 
	}
}

//This method will hide and show the panel.	
function HideShowPanel(Panel)
{
	try
	{
		var ChosenPanel = getElement(Panel);
		if ( ChosenPanel != null )
		{
			var currentdisplay = getElement(Panel).style.display;
			if ( currentdisplay != "block")
			{
				getElement(Panel).style.display = "block";
			}
			else
			{
				getElement(Panel).style.display = "none";
			}
		}
	}
	catch(e)
	{
		alert( "Error in HideShowPanel Method: " + e); 
	}
}

//This method will get the object using partial id match.
function getItem(IDSearchCriteria, DataGridID) 
{
	try
	{
		if ( IDSearchCriteria == null || IDSearchCriteria.length <= 0 )
		{
			return null;
		}
		else if ( DataGridID == null || DataGridID.length <= 0 )
		{
			return null;
		}
		
		if( document.getElementsByTagName )
		{  
			var table = document.getElementById(DataGridID);   
			if ( table != null )
			{
				var rows = table.getElementsByTagName("tr");   
				for ( var i = 0; i < rows.length; i++ )
				{
					var Identity = rows[i].id;
					
					if ( Identity != null && Identity.length > 0 )
					{
						
						var cellid = IDSearchCriteria.substring( IDSearchCriteria.lastIndexOf("_") + 1 );
						if ( Identity.match(cellid) )
						{
							//alert("Found: " + Identity);
							return Identity;
						}
						
					}
				}
			}
			return null;      
		}
		else
		{
			return null;
		}
	}
	catch(e)
	{ 
		alert( "Error in getItem Method: " + e); 
		return null;
	}
}