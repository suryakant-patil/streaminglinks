//----------------------------------------------------------

try
{
// Create  New Array for storing Top banners
 var TopBanners1= new Array() 

 TopBanners1[0] = '<iframe allowtransparency="true" src=\'http://ff.connextra.com/BlueSquare/selector/client?client=BlueSquare&amp;placement=Takearisk_generic_728x90\' width=\'728\' height=\'90\' scrolling=\'no\' frameborder=\'no\' style=\'border-width:0\'></iframe>'; TopBanners1[1] = '<A HREF="http://www.bet365.com/home/default.asp?affiliate=grm_3959&amp;oty=1&amp;tzi=1&amp;lng=1" TARGET="_blank" onMouseOver="window.status=\'http://www.bet365.com/home/default.asp\'; return true" onMouseOut="window.status=\'Done\'; return true"><IMG SRC="http://www.ads365.com/creative/728x90/2005/728x90_0507_free100pound_basket.gif" BORDER="0" alt="Bet It Live With bet365!"></A>';

// find the numbers of the Top banners1 
var i = Math.random() * this.TopBanners1.length;
 i = parseInt(i);

//alert ( parseInt(i) + " *** " + this.TopBanners.length);
  // write the banner to span
 
  document.all.bannertop1.innerHTML = TopBanners1[i];
  
 }
 catch(e)
 {
	document.all.bannertop1.innerHTML = " Top Banner1" ;
 } 
 
// alert(TopBanners[i]);
//----------------------------------------------------------  

try
{

// Create  New Array for storing Top banners2
 var TopBanners2= new Array() 



// find the numbers of the Top banners1 
var i = Math.random() * this.TopBanners2.length;
 i = parseInt(i);

//alert ( parseInt(i) + " *** " + this.TopBanners.length);
  // write the banner to span
 
  document.all.bannertop2.innerHTML = TopBanners2[i];
  
 }
 catch(e)
 {
	document.all.bannertop2.innerHTML = "Top Banner";
 } 
// alert(TopBanners[i]);
//---------------------------------------------------------- 
 
 try
 {
  // Create  New Array for storing Right banners
 var RightBanners= new Array() 



// find the numbers of the Right banners 
var j = Math.random() * this.RightBanners.length;
 j = parseInt(j);

//alert ( parseInt(j) + " *** " + this.RightBanners.length);
  // write the Right banner to span
 
  document.all.bannerright.innerHTML = RightBanners[j];
  
 }
 catch(e)
 {
	document.all.bannerright.innerHTML = "Right Banner"
 } 
  
//  alert (RightBanners[j]);

//----------------------------------------------------------

// Create  New Array for storing middle banners
try
{
 var MiddleBanners= new Array() 

  MiddleBanners[0] =' <iframe allowtransparency="true" src=\'http://ff.connextra.com/Ladbrokes/selector/client?client=Ladbrokes&amp;placement=BettingChoice_Sportsbook_120x600\' width=\'120\' height=\'600\' scrolling=\'no\' frameborder=\'no\' style=\'border-width:0\'></iframe><br><iframe allowtransparency="true" src=\'http://ff.connextra.com/Ladbrokes/selector/client?client=Ladbrokes&amp;placement=BettingChoice_Sportsbook_120x600\' width=\'120\' height=\'600\' scrolling=\'no\' frameborder=\'no\' style=\'border-width:0\'></iframe>'; MiddleBanners[1] =' <table cellpadding=2 cellspacing=2 border=0><tr><td><A HREF="http://www.bet365.com/home/default.asp?affiliate=grm_3959&amp;oty=1&amp;tzi=1&amp;lng=1" TARGET="_blank" onMouseOver="window.status=\'http://www.bet365.com/home/default.asp\'; return true" onMouseOut="window.status=\'Done\'; return true"><IMG SRC="http://www.ads365.com/creative/120x600/2005/120x600_0507_free100pound_basket.gif" BORDER="0" alt="Bet It Live With bet365!"></A></td></tr><tr><td><A HREF="http://www.bet365.com/home/default.asp?affiliate=grm_3959&amp;oty=1&amp;tzi=1&amp;lng=1" TARGET="_blank" onMouseOver="window.status=\'http://www.bet365.com/home/default.asp\'; return true" onMouseOut="window.status=\'Done\'; return true"><IMG SRC="http://www.ads365.com/creative/120x600/2005/120x600_0507_free100pound_basket.gif" BORDER="0" alt="Bet It Live With bet365!"></A></td></tr></table>';
  
// find the numbers of the Right banners 
 var k = Math.random() * this.MiddleBanners.length;
 k = parseInt(k);

  // alert ( parseInt(k) + " *** " + this.MiddleBanners.length);
  // write the Right banner to span

  document.all.bannermiddle.innerHTML = MiddleBanners[k];
  //alert (MiddleBanners[k]);
    
}
 catch(e)
 {
  document.all.bannermiddle.innerHTML = "Middle Banners"
 } 
