﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="News.aspx.vb" Inherits="News" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>SEPA</title>
<meta name="keywords" content="sepa" />
<meta name="description" content="SEPA" />
<link href="sepa_style.css" rel="stylesheet" type="text/css" />
<!--  Designed by w w w . t e m p l a t e m o . c o m  -->
<link rel="stylesheet" type="text/css" href="contentslidernews.css" />
<script type="text/javascript" src="contentslider.js">
/***********************************************
* Featured Content Slider- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit Dynamic Drive at http://www.dynamicdrive.com/ for this script and 100s more
***********************************************/
</script>
<!--[if lt IE 7]>
<style type="text/css">

  .sepa_thumb_box span { behavior: url(iepngfix.htc); }

</style>
<![endif]-->
</head>

<body>
<div id="sepa_container">
	
<div id="sepa_header_panel">
        <div id="sepa_title_section">
            
        </div>
        <div id="sepa_top_destra_section">
            <ul>
              <li><a href="javascript:top.location.href='Portale.aspx';">Home</a></li>
              <li><a href="javascript:top.location.href='AreaPrivata.aspx';">Area Privata</a></li>
              <li><a href="#">&nbsp;&nbsp;News&nbsp;&nbsp;</a></li>
            </ul>
        </div>
    </div>
    
    <div id="sepa_login_banner_panel">

        <div id="sepa_banner_panel_news">
        
            <!--Inner content DIVs should always carry "contentdiv" CSS class-->
            <!--Pagination DIV should always carry "paginate-SLIDERID" CSS class-->
            
            <div id="paginate-slider2" class="pagination">
            
            <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc anotherclass">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a> <a href="#" class="toc">&nbsp;</a>
            </div>
            
            <div id="slider2" class="sliderwrapper">
              <div class="contentdiv"><img src="images/contatti/contatti_1.jpg" alt="image 10" /></div>
              <div class="contentdiv"><img src="images/contatti/contatti_2.jpg" alt="image 20" /></div>
              <div class="contentdiv"><img src="images/contatti/contatti_3.jpg" alt="image 30" /></div>
              <div class="contentdiv"><img src="images/contatti/contatti_4.jpg" alt="image 40" /></div>
              <div class="contentdiv"><img src="images/contatti/contatti_5.jpg" alt="image 50" /></div>
              <div class="contentdiv"><img src="images/contatti/contatti_6.jpg" alt="image 60" /></div>
              <div class="contentdiv"><img src="images/contatti/contatti_7.jpg" alt="image 70" /></div>
            </div>
          <script type="text/javascript">
            
            featuredcontentslider.init({
            id: "slider2",  //id of main slider DIV
            contentsource: ["inline", ""],  //Valid values: ["inline", ""] or ["ajax", "path_to_file"]
            toc: "markup",  //Valid values: "#increment", "markup", ["label1", "label2", etc]
            nextprev: ["Previous", "Next"],  //labels for "prev" and "next" links. Set to "" to hide.
            revealtype: "click", //Behavior of pagination links to reveal the slides: "click" or "mouseover"
            enablefade: [true, 0.2],  //[true/false, fadedegree]
            autorotate: [true, 3000],  //[true/false, pausetime]
            onChange: function(previndex, curindex){  //event handler fired whenever script changes slide
            //previndex holds index of last slide viewed b4 current (1=1st slide, 2nd=2nd etc)
            //curindex holds index of currently shown slide (1=1st slide, 2nd=2nd etc)
            }
            })
            
            </script>
        </div>
        </div>
    <!-- end of login and banner panel -->
    
	<div id="sepa_menu_news">
        
    </div>

	<!-- content -->
    <div id="sepa_content">
    	<div id="sepa_leftcolumn_news">
        	<div class="sepa_leftcolumn_allnews">

			  
			  <%= psNews %>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
          </div>
            <div class="cleaner"> &nbsp; </div>
            
          <div class="sepa_leftcolumn_fullrow">
	            <h1>&nbsp;</h1>
              <p><a href="http://get.adobe.com/it/reader/"  target="_blank"><img src="Images/get_adobe_reader.png" alt="image" border="0" /></a></p>
            <p>Per una corretta visualizzazione dei documenti nel formato PDF è necessario l'Adobe Acrobat Reader </p>
              
          </div> <!-- end of left full column -->
            
        </div> <!-- end of left column -->
      <!-- end of right column --> 
  </div>
    <!-- end of content -->
    <div id="sepa_footer">
        <a href="#">Home</a> | <a href="#">Chi Siamo</a> | <a href="#">Policy</a> | <a href="#">Help</a> | <a href="#">FAQs</a> | <a href="#">Contatti</a><br />
        Copyright Â© 2009 <a href="#"><strong>SEPA@WEB</strong></a> | Designed by <a href="http://www.sistemiesoluzionisrl.it" target="_blank">Sistemi e Soluzioni</a>
    </div>
<!--  Designed by sistemi e soluzioni srl  -->
</div> <!-- end of container -->
</body>
</html>
