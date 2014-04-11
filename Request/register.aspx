<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
    <title>Avaya Technology Forum 2014</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="Daniel Turriago (daniel@turriago.com) - Avaya Inc. 2013">

    <!-- Le styles -->
    <link href="assets/css/bootstrap.css" rel="stylesheet">
    <link href="assets/css/bootstrap-responsive.css" rel="stylesheet">
    <link href="assets/css/docs.css" rel="stylesheet">
    <link href="assets/css/style.css" rel="stylesheet">
    <link href="css/messi.css" rel="stylesheet">
    <link href="css/prettyLoader.css" rel="stylesheet">
       <!--[if IE 8 ]>    <html class="ie8"> <![endif]-->
	<!--[if IE 7 ]>    <html class="ie7"> <![endif]-->
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
   
      <script src="assets/js/html5shiv.js"></script>
    <![endif]-->

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/ico/apple-touch-icon-114-precomposed.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/ico/apple-touch-icon-72-precomposed.png">
                    <link rel="apple-touch-icon-precomposed" href="assets/ico/apple-touch-icon-57-precomposed.png">
                                   <link rel="shortcut icon" href="assets/ico/favicon.png">
  </head>

  <body>



  <!-- NAVBAR
    ================================================== -->
   <div class="navbar navbar-inverse navbar-fixed-top">
      <div class="navbar-inner">
        <div class="container">
          
          
          <a class="brand" href="http://avaya.com"><img class="desktop" src="assets/img/avaya-logo.jpg" alt="Avaya" /><img class="mobile" src="assets/img/avaya-logo-mobile.jpg" alt="Avaya" /></a>
	  
	   <button type="button" class="btn btn-navbar visible-phone" data-toggle="collapse" data-target=".nav-collapse">
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
            </button>
          
          <!-- Responsive Navbar Part 2: Place all navbar contents you want collapsed withing .navbar-collapse.collapse. -->
            <div class="nav-collapse collapse visible-phone">
              <ul class="nav">
                 <li><a href="index.html">HOME</a></li>
		<li><a href="reasons.html">REASONS TO ATTEND</a></li>
              <li><a href="logistics.html">LOGISTICS</a></li>
	      <li><a href="sessions.html">SESSIONS</a></li>
	      <li><a href="agenda.aspx">AGENDA</a></li>
              <li class="register"><a href="register.aspx">REGISTER</a></li>
                  </ul>
                </li>
              </ul>
            </div>
      
      <!--/.nav-collapse -->
          
        </div>
      </div>
    </div>



    <!-- Carousel
    ================================================== -->
    <div class="container">
    
    <div id="myCarousel" class="carousel slide">
      
     

          <!-- Carousel items -->
      <div class="carousel-inner">
        <div class="item first-item active">
          <div class="container">
            <div class="carousel-caption red-box">
              <h1>Avaya Technology Forum</h1>
              <p class="lead">March 25 - 27, 2014</p>
              <!--<a target="_blank" href="#">Register Now ></a>-->
            </div>
          </div>
        </div>
        <div class="item second-item">
          <div class="container">
            <div class="carousel-caption red-box">
              <h1>Marriott Renaissance</h1>
              <p class="lead">Orlando at SeaWorld</p>
              <!--<a target="_blank" href="#">Register Now ></a>-->
            </div>
          </div>
        </div>
	<div class="item third-item">
          <div class="container">
            <div class="carousel-caption red-box">
              <h1>Brett Shockley</h1>
              <p class="lead">Senior VP and CTO, Avaya</p>
              <!--<a target="_blank" href="#">Register Now ></a>-->
            </div>
          </div>
        </div>
	<div class="item fourth-item">
          <div class="container">
            <div class="carousel-caption red-box">
             <h1>Andrew Lerner </h1>
              <p class="lead">Research Director, Gartner Research</p>
              <!--<a target="_blank" href="#">Register Now ></a>-->
            </div>
          </div>
        </div>
        
        
        
      </div>
      <!-- Carousel Indicators -->
      
      <!--<a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
      <a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a>-->
      
       <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
		<li data-target="#myCarousel" data-slide-to="2"></li>
		<li data-target="#myCarousel" data-slide-to="3"></li>
            </ol>
      
    </div>
    
    </div>
    
    <!-- /.carousel -->



    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->

    <div class="container">
      <div class="row-fluid">
       <div class="span3">
          <div class="sidebar-nav">
            <ul class="nav nav-list bs-docs-sidenav"  data-spy="affix" data-offset-top="470" data-offset-bottom="280">
              <li class="hidden-phone"><a href="index.html">HOME</a></li>
              <li class="hidden-phone"><a href="reasons.html">REASONS TO ATTEND</a></li>
              <li class="hidden-phone"><a href="logistics.html">LOGISTICS</a></li>
	      <li class="hidden-phone"><a href="sessions.html">SESSIONS</a></li>
	      <li class="hidden-phone"><a href="agenda.aspx">AGENDA</a></li>
              <li class="register"><a href="#">REGISTER</a></li>
            <li class="calendar"><a href="FY14_Avaya_Technology_Forum"><img style="vertical-align: bottom;" src="assets/img/calendar.png">ADD TO CALENDAR</a></li>
              <li class="days">DAYS LEFT TO CONFERENCE</li>
              <li class="countdown">
                <!--Inicio del contador-->
                <table cellpadding="0" cellspacing="0" width="90%" bgcolor="#eceeef" align="center">
                  <tr>
                    <td nowrap align="center" style="width: 35%;"><span id="dias"></span></td>
                    <td colspan="3" nowrap align="center" style="width: 65%;"><span id="tiempo">&nbsp;</span></td>
                  </tr>
                  <tr>
                    <td>DAYS</td>
                    <td>HRS</td>
                    <td>MIN</td>
                    <td>SEC</td>
                  </tr>
                </table>
                <!--Fin del contador-->
              </li>
            </ul>
              
          </div><!--/.well -->
        </div><!--/span-->
        
        <div class="span9">
          
          <hr style="margin-top: 0;">
           
           <div class="row-fluid">
            <div class="span12">
              <h2>Pre-Register for the Conference</h2>
         
	             <div class="form-horizontal">
	                 <div class="control-group">
                        <label class="control-label" type="email" for="inputEmail"><span style="color:#cc0000;">*</span>Email:</label>
                        <div class="controls">
                          <input type="text" id="inputEmail" placeholder="Email">
                        </div>
                        <div class="controls">
                        <small>Note: Unique and accurate email addresses are required for each attendee. DO NOT register multiple persons using the same email address.</small>
                        </div>
                    </div>
		  
                    <div class="control-group">
                    <label class="control-label" for="inputName"><span style="color:#cc0000;">*</span>First Name:</label>
                    <div class="controls">
                      <input type="text" id="inputName" placeholder="First Name">
                    </div>
                  </div>
                    <div class="control-group">
                    <label class="control-label" for="inputLast"><span style="color:#cc0000;">*</span>Last Name:</label>
                    <div class="controls">
                      <input type="text" id="inputLast" placeholder="Last Name">
                    </div>
                  </div>
                     <div class="control-group">
                    <label class="control-label" for="inputPhone"><span style="color:#cc0000;">*</span>Telephone:</label>
                    <div class="controls">
                      <input type="text" id="inputPhone" placeholder="Phone">
                    </div>
                  </div>
		     <div class="control-group">
                    <label class="control-label" for="inputAddress"><span style="color:#cc0000;">*</span>Address:</label>
                    <div class="controls">
                      <input type="text" id="inputAddress" placeholder="Address">
                    </div>
                  </div>
                    <div class="control-group">
                    <label class="control-label" for="inputCompany"><span style="color:#cc0000;">*</span>Company:</label>
                    <div class="controls">
                      <input type="text" id="inputCompany" placeholder="Company">
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="inputJob"><span style="color:#cc0000;">*</span>Title:</label>
                    <div class="controls">
                      <input type="text" id="inputJob" placeholder="Job Position">
                    </div>
                  </div>
  
                   <div class="control-group">
    <label class="control-label" for="inputCountry"><span style="color:#cc0000;">*</span>Country:</label>
    <div class="controls">
           <select id="country">
		              <option value="" selected="selected"></option>
					  <option value="Anguila">Anguila</option>
					  <option value="Antigua and Barbuda">Antigua and Barbuda</option>
					  <option value="Argentina" >Argentina</option>
					  <option value="Aruba">Aruba</option>
					  <option value="Bahamas">Bahamas</option>
					  <option value="Barbados">Barbados</option>
					  <option value="Belize">Belize</option>
					  <option value="Bermuda">Bermuda</option>
					  <option value="Bolivia">Bolivia</option>
					  <option value="Brazil">Brazil</option>
					  <option value="Canada">Canada</option>
					  <option value="Cayman Islands">Cayman Islands</option>
					  <option value="Chile" >Chile</option>
					  <option value="Colombia">Colombia</option>
					  <option value="Costa Rica">Costa Rica</option>
					  <option value="Cuba">Cuba</option>
					  <option value="Curacao">Curacao</option>
					  <option value="Dominica">Dominica</option>
					  <option value="Dominican Republic">Dominican Republic </option>
					  <option value="Ecuador">Ecuador</option>
					  <option value="El Salvador">El Salvador</option>
					  <option value="Grenada">Grenada</option>
					  <option value="Guadeloupe">Guadeloupe</option>
					  <option value="Guatemala">Guatemala</option>
					  <option value="Guyana">Guyana</option>
					  <option value="Haiti">Haiti</option>
					  <option value="Honduras">Honduras</option>
					  <option value="Jamaica">Jamaica</option>
					  <option value="Mexico">Mexico</option>
					  <option value="Netherlands Antilles">Netherlands Antilles</option>
					  <option value="Nicaragua">Nicaragua</option>
					  <option value="Panama">Panama</option>
					  <option value="Paraguay">Paraguay</option>
					  <option value="Peru">Peru</option>
					  <option value="Puerto Rico">Puerto Rico</option>
					  <option value="Saint Barthelemy">Saint Barthelemy</option>
					  <option value="Saint Helena">Saint Helena</option>
					  <option value="Saint Kitts and Nevis">Saint Kitts and Nevis</option>
					  <option value="Saint Lucia">Saint Lucia</option>
					  <option value="Saint Martin">Saint Martin</option>
					  <option value="Saint Pierre y Miquelon">Saint Pierre y Miquelon</option>
					  <option value="Saint Vincent and The Grenadines">Saint Vincent and The Grenadines</option>
					  <option value="Trinidad and Tobago">Trinidad and Tobago</option>
					  <option value="Turks and Caicos Islands">Turks and Caicos Islands</option>
					  <option value="United States">United States</option>
					  <option value="Uruguay">Uruguay</option>
					  <option value="Venezuela">Venezuela</option>
					  <option value="Virgin Islands British">Virgin Islands British</option>
					  <option value="Virgin Islands U.S.">Virgin Islands U.S.</option>
</select>
    </div>
  </div>
  
                  <div class="control-group">
                    <label class="control-label" for="inputCountry"><span style="color:#cc0000;">*</span>Organization:</label>
                    <div class="controls">
                        <select id="organization">
                          <option value="" selected="selected">Select Organization from below</option>
                          <option value="Customer">Customer</option>
                          <option value="Consultant" >Consultant</option>
                          <option value="Analyst">Analyst</option>
                          <option value="Partner - Reseller">Partner - Reseller</option>
                          <option value="Partner - Distributor">Partner - Distributor</option>
                          <option value="Partner - SI/SP">Partner - SI/SP</option>
                          <option value="Partner - DevConnect">Partner - DevConnect</option>
                          <option value="Avaya Systems Engineer">Avaya Systems Engineer</option>
                          <option value="Avaya Sales">Avaya Sales</option>
                          <option value="Avaya Channel Systems Engineer">Avaya Channel Systems Engineer</option>
                          <option value="Avaya Channel Sales">Avaya Channel Sales</option>
                          <option value="Avaya SE Management">Avaya SE Management</option>
                          <option value="Avaya Sales Management">Avaya Sales Management</option>
                          <option value="Avaya CESG">Avaya CESG</option>
                          <option value="Avaya CTO">Avaya CTO</option>
                          <option value="Avaya Product Management">Avaya Product Management</option>
                          <option value="Avaya Marketing">Avaya Marketing</option>
                          <option value="Avaya R&D">Avaya R&D</option>
                          <option value="Avaya Client Services">Avaya Client Services</option>
                          <option value="Avaya Professional Services">Avaya Professional Services</option>
                          <option value="Avaya EBC/Demo Avaya">Avaya EBC/Demo Avaya</option>
                          <option value="Avaya Tech Ops Programs">Avaya Tech Ops Programs</option>
                          <option value="Avaya Sales Factory">Avaya Sales Factory</option>
                          <option value="Avaya Training">Avaya Training</option>
                          <option value="Other">Other</option>
                       </select>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="inputCountry"><span style="color:#cc0000;">*</span>Conference Role:</label>
                    <div class="controls">
                        <select id="conference_role">
                            <option value="" selected="selected">Select Conference Role from below</option>
                            <option value="Attendee">Attendee</option>
                            <option value="Exhibitor" >Exhibitor</option>
                            <option value="Speaker">Speaker</option>
                            <option value="Conference Support">Conference Support</option>
                            <option value="Other">Other</option>
                        </select>
                    </div>
                  </div>
  
                <div class="control-group">
                  <label class="control-label" for="inputWorkshops"><span style="color:#cc0000;">*</span>Will you be staying at the Conference Hotel?</label>
  
                  <div id="inputWorkshops" class="controls">
                      <label class="radio inline">
                        <input type="radio" name="inputWorkshops" id="inputWorkshops1" value="yesworkshop">yes
                      </label>
                      <label class="radio inline">
                        <input type="radio" name="inputWorkshops" id="inputWorkshops2" value="noworkshop">no
                      </label>
                  </div>
                  <div class="controls"><small>If you are not staying at the hotel, the Conference Fee must be paid in full by credit card at conference check-in prior to attending any sessions.</small></div>
                </div>

                <div class="control-group">
                    <label class="control-label" for="inputForum"><span style="color:#cc0000;">*</span>Will you be attending the Pre-Forum Platinum Sponsor Workshops on Monday afternoon?</label>
                    <div id="inputForum" class="controls">
                        <label class="radio inline">
                          <input type="radio" name="inputForum" id="inputForum1" value="yesForum">yes
                        </label>
                        <label class="radio inline">
                          <input type="radio" name="inputForum" id="inputForum2" value="noForum">no
                        </label>
                    </div>
                </div>
  
                <!--<div class="control-group">
                    <label class="control-label" for="inputdiet">Special Diet:</label>
                    <div class="controls">
                      <input type="text" id="inputdiet">
                    </div>
                </div>-->
		
		
		        <div class="control-group">
                  <label class="control-label" for="inputdiet"><span style="color:#cc0000;"></span>Special Diet:</label>
		  
		            <div id="inputdiet" class="controls">
                        <label class="radio inline">
                        <input type="radio" name="inputdiet" id="inputdiet1" value="glutenfree">Gluten Free
                        </label>
                        <label class="radio inline">
                        <input type="radio" name="inputdiet" id="inputdiet2" value="kosher">Kosher
                        </label>
		                <label class="radio inline">
                        <input type="radio" name="inputdiet" id="inputdiet3" value="vegetarian">Vegetarian
                        </label>
		                <label class="radio inline">
                        <input type="radio" name="inputdiet" id="inputdiet4" value="other">Other
			            <input type="text" id="inputdiet5" placeholder="Other Option" style="visibility:hidden;">
                       </label>
                    </div>
		        </div>
                
                <div class="control-group">
                    <label class="control-label" for="inputemergency">Emergency Contact:</label>
                    <div class="controls">
                      <input type="text" id="inputEmergency">
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="inputEmergencyNum">Emergency Contact #:</label>
                    <div class="controls">
                      <input type="text" id="inputEmergencyNum">
                    </div>
                </div>

                <div class="control-group">
                    <label class="control-label" for="inputTwitter">Twitter Username:</label>
                    <div class="controls">
                      <input type="text" id="inputTwitter">
                    </div>
                </div>
    
                <div id="inputInfo" class="control-group">
                    <div class="controls">
                        <label class="checkbox inline">
                            <input type="checkbox" name="info" id="inputInfo1" value="yesInfo">Yes, I would like to receive email information from the event sponsors with the understanding that I can opt out at any time.
                        </label>
                    </div>
                    <div class="controls">
                        <label class="checkbox inline">
                            <input type="checkbox" name="info" id="inputInfo2" value="noInfo">No, do not send sponsor information.
                        </label>
                    </div>
                </div>

                  <div class="control-group">
                    <div class="controls">
                  <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="Register">Submit Now</button>
                  </div>
                  </div>
            </div>
	 
	 
            </div>
           </div>
	   
            
        </div>
      </div>
    </div>
    
    
    <!-- /.container -->
    
            <!-- Footer
    ================================================== -->
    <footer class="footer">
      
      <div class="container visible-desktop">
        <div class="row" style="padding: 10px 0;">
      <div class="span4 info">
         <table cellpadding="0" cellspacing="0" align="center">
          <tr>
            <td><!--<img style="float: none; padding-bottom: 8px" src="assets/img/footer/phone.png"></td>
            <td align="left">1-866-535-2542 (US only toll-free)
        <br>+1-212-653-1260 (Outside the US)-->
	     <a class="hashtag" href="https://twitter.com/intent/tweet?button_hashtag=AvayaATF&text=I'm%20going%20to" class="twitter-hashtag-button" data-related="Avaya">#AvayaATF</a>
	    </td>
	
          </tr>
        </table>
      </div>
       <div class="span4 info">
         <!--<img style="float: none; padding-bottom: 8px" src="assets/img/footer/mail.png">
        <a href="mailto:conferenceHeadquarters@madisonpg.com">ConferenceHeadquarters@madisonpg.com</a>-->
      </div>
      <div class="span4 info">
      <a href="https://www.facebook.com/avaya"><img src="assets/img/social/facebook.png"></a>
      <a href="http://twitter.com/avaya"><img src="assets/img/social/twitter.png"></a>
      <a href="http://www.linkedin.com/company/1494"><img src="assets/img/social/linkedin.png"></a>
      <a href="http://www.youtube.com/Avayainteractive"><img src="assets/img/social/youtube.png"></a>
      <a href="http://www.flickr.com/photos/avaya"><img src="assets/img/social/flickr.png"></a>
      <a href="http://www.avaya.com/blogs/"><img src="assets/img/social/blog.png"></a>
      </div>
    </div>
      </div>
      
      <div class="container hidden-desktop">
        <div class="row" style="padding: 20px 0;">
      <div class="span6 info">
         <table cellpadding="0" cellspacing="0" align="center">
          <!--<tr>
            <td><img style="float: none; padding-bottom: 8px" src="assets/img/footer/phone.png"></td>
            <td align="left">1-866-535-2542 (US only toll-free)
        <br>+1-212-653-1260 (Outside the US)</td>
          </tr>-->
        </table>
      </div>
       <div class="span6 info">
         <!--<img style="float: none; padding-bottom: 8px" src="assets/img/footer/mail.png">
        <a href="mailto:conferenceHeadquarters@madisonpg.com">ConferenceHeadquarters@madisonpg.com</a>-->
      </div>
    </div>
      </div>
      
      <div class="bs-docs-social social-media">
  <div class="container">
    
    <div class="row  hidden-desktop">
      <div class="span12">
        <ul class="bs-docs-social-buttons">
      <li ><a href="https://www.facebook.com/avaya"><img src="assets/img/social/facebook-mobile.png"></a></li>
      <li><a href="http://twitter.com/avaya"><img src="assets/img/social/twitter-mobile.png"></a></li>
      <li><a href="http://www.linkedin.com/company/1494"><img src="assets/img/social/linkedin-mobile.png"></a></li>
      <li><a href="http://www.youtube.com/Avayainteractive"><img src="assets/img/social/youtube-mobile.png"></a></li>
      <li><a href="http://www.flickr.com/photos/avaya"><img src="assets/img/social/flickr-mobile.png"></a></li>
      <li><a href="http://www.avaya.com/blogs/"><img src="assets/img/social/blog-mobile.png"></a></li>
    </ul>
      </div>
    </div>
    
      
    <div class="row">
            
            <div class="span12">
              <ul class="footer-links">
              <li><a href="https://thesource.avaya.com/avayaPortal/friendly/termsPage">Terms of Use</a></li>
              <li class="muted">&middot;</li>
              <li><a href="http://www.avaya.com/gcm/master-usa/en-us/includedcontent/privacy.htm">Privacy Statement</a></li>
              <li class="muted">&middot;</li>
              <li><a href="http://www.avaya.com/gcm/master-usa/en-us/includedcontent/cookiepolicy.htm">Cookies</a></li>
              <li class="muted">&middot;</li>
              <li class="muted">&copy; 2009-2013 Avaya Inc.</li>
            </ul>
            </div>
        </div>
    
    </div>
  </div>
</div>
      
      
      
    </footer>




    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="js/jquery.js"></script>
    <script src="js/register.js"></script>
    <script src="js/messi.js"></script>
    <script src="assets/js/bootstrap-transition.js"></script>
    <script src="assets/js/bootstrap-alert.js"></script>
    <script src="assets/js/bootstrap-modal.js"></script>
    <script src="assets/js/bootstrap-dropdown.js"></script>
    <script src="assets/js/bootstrap-scrollspy.js"></script>
    <script src="assets/js/bootstrap-tab.js"></script>
    <script src="assets/js/bootstrap-tooltip.js"></script>
    <script src="assets/js/bootstrap-popover.js"></script>
    <script src="assets/js/bootstrap-button.js"></script>
    <script src="assets/js/bootstrap-collapse.js"></script>
    <script src="assets/js/bootstrap-carousel.js"></script>
    <script src="assets/js/bootstrap-typeahead.js"></script>
    <script src="assets/js/bootstrap-affix.js"></script>
    <script src="assets/js/mystyle.js"></script>
    <script language="javascript" src="assets/js/reloj.js"></script>
    <script>faltan()</script>
    <script>
      !function ($) {
        $(function(){
          // carousel demo
          $('#myCarousel').carousel()
        })
      }(window.jQuery)
    </script>
    <script src="assets/js/holder/holder.js"></script>
    <script src="assets/js/respond.src.js"></script>
    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->
  </body>
</html>
