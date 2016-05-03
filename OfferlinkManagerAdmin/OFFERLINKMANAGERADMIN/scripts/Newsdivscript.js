

var _elementID;
var _xOffset = 15;
var _yOffset = 15;

function GetMouseX( event )
{ 
 if ( !event )  
  {
   event = window.event;
  }    
  if ( event.pageX )
   {
    
    return event.pageX;
   }  
   else if ( event.clientX )   
   { 
   
   return event.clientX + ( document.documentElement.scrollLeft ?  document.documentElement.scrollLeft : document.body.scrollLeft ); 
   }
   else    {        return 0;    }}
              
function GetMouseY( event )
{   
    if ( !event )
        {       
         event = window.event; 
         }     
   if ( event.pageY )  
    {
        return event.pageY;  
    } 
    else if ( event.clientY )   
    {   
        return event.clientY + ( document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop );    
        
        } 
    else 
    { 
    return 0;  
    }
        }
function Follow( event )
{   
 if ( document.getElementById )   
{ 
         var element = document.getElementById( _elementID ); 
            if ( element != null )        
            {            
                         var style = element.style;
                        // alert('x'+ parseInt( GetMouseX( event ) ));
                         //alert('Y'+ parseInt( GetMouseY( event ) ));
                         
                         
                        // style.left = ( parseInt( GetMouseX( event ) ) + _xOffset ) + 'px';    
                         //style.top = ( parseInt( GetMouseY( event ) ) + _yOffset ) + 'px';
                        style.left = ( parseInt( GetMouseX( event ) ) + 10  ) + 'px';    
                        style.top = ( parseInt( GetMouseY( event ) ) - 50 ) + 'px';
                       // style.OVERFLOW ='auto';
                         //style.left = 350+ 'px';    
                         //style.top = 300 + 'px';
                         style.visibility = 'visible';
                         
                         
              }
              
  }               
               }
                    
                
function Show( elementID )
{
    if ( document.getElementById ) 

{             _elementID = elementID;              
        document.onmousemove = Follow;    }}

function Hide( elementID )
{    if ( document.getElementById )   
{       
      _elementID = elementID;                
    var divStyle = document.getElementById( _elementID ).style;  
    divStyle.visibility = 'hidden';        
     document.onmousemove = '';    }}