<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sol_details.aspx.cs" Inherits="sol_details" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
    <title>My Profile - Avaya</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="developer" content="William Ballesteros Blanco (wballesteros@avaya.com) - Avaya Inc. 2014">

    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">
    
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet">
    <link href="css/docs.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/messi.css" rel="stylesheet">
    <link href="css/prettyLoader.css" rel="stylesheet">
    <link href="css/DT_bootstrap.css" rel="stylesheet" type="text/css"/>
    <link href="css/bootstrap-dialog.css" rel="stylesheet" type="text/css" />
    

    

    <style>
        .btn-file {
            position: relative;
            overflow: hidden;
        }

        .btn-file input[type=file] {
            position: absolute;
            top: 0;
            right: 0;
            min-width: 150%;
            min-height: 100%;
            font-size: 999px;
            text-align: right;
            filter: alpha(opacity=0);
            opacity: 0;
            background: red;
            cursor: inherit;
            display: block;
        }
    </style>

    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/bootstrap-dialog.js"></script>
    <script type="text/javascript" src="js/mystyle.js"></script>
    <script type="text/javascript" src="js/holder/holder.js"></script>
    <script type="text/javascript" src="js/respond.src.js"></script>
    <script type="text/javascript" src="js/prettyLoader.js"></script>
    <script type="text/javascript" src="js/ajaxfileupload.js"></script>
    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/DT_bootstrap.js"></script>
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>
    <script type="text/javascript" src="js/sol_details.js"></script>

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


                <a class="brand" href="http://avaya.com">
                    <img class="desktop" src="images/avaya-logo.jpg" alt="Avaya" /><img class="mobile" src="images/avaya-logo-mobile.jpg" alt="Avaya" /></a>

                <button type="button" class="btn btn-navbar visible-phone" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

                <div class="nav-collapse collapse">
                    <ul class="nav">
                        <li><a href="solicitante.aspx" id="my_solicits">My Requests</a></li>
                        <li><a href="sol_details.aspx" id="information">My Profile</a></li>
                        <li><a href="#" id="exit">Exit</a></li>
                        <%--<li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" id="my_profile">My Profile&nbsp;&nbsp;<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="sol_details.aspx" id="information">Information</a></li>
                                <li><a href="#" id="exit">Exit</a></li>
                            </ul>
                         </li>--%>
                    </ul>
                </div>

               
                
                <!--/.nav-collapse -->

            </div>
        </div>
    </div>


    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->

    <div class="container" id="profile_data">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">

                <div class="row-fluid">
                    <div class="span12">
                        <h2>My Profile</h2>

                        <div class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label" for="user_id"><span style="color: #cc0000;">*</span>User:</label>
                                <div class="controls">
                                    <input id="user_id" type="text" placeholder="User Id" disabled />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="role"><span style="color: #cc0000;">*</span>Role:</label>
                                <div class="controls">
                                    <input id="role" type="text" placeholder="Role" disabled />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="Firstname"><span style="color: #cc0000;">*</span>Firstname:</label>
                                <div class="controls">
                                    <input id="Firstname" type="text" placeholder="Firstname" disabled />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="Lastname"><span style="color: #cc0000;">*</span>Lastname:</label>
                                <div class="controls">
                                    <input id="Lastname" type="text" placeholder="Lastname" disabled />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="Email"><span style="color: #cc0000;">*</span>Email:</label>
                                <div class="controls">
                                    <input id="Email" type="text" placeholder="Email" disabled />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="Address"><span style="color: #cc0000;">*</span>Address:</label>
                                <div class="controls">
                                    <input id="Address" type="text" placeholder="Address" disabled />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="Phone"><span style="color: #cc0000;">*</span>Phone:</label>
                                <div class="controls">
                                    <input id="Phone" type="text" placeholder="Phone" disabled />
                                </div>
                            </div>

                            
                        </div>


                    </div>
                </div>


            </div>
        </div>
    </div>

    

    <!-- Progress bar -->
    <div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-header">
                <h1>Processing...</h1>
            </div>
            <div class="modal-body">
                <div class="progress progress-striped active">
                    <div class="bar progress-bar-danger" style="width: 100%;"></div>
                </div>
            </div>
    </div>

    <!-- Footer
    ================================================== -->
    <footer class="footer">

        <div class="container visible-desktop">
            <div class="row" style="padding: 10px 0;">

                <div class="span4 info">
                    
                </div>
                <div class="span4 info">
                    <a href="https://www.facebook.com/avaya">
                        <img src="images/social/facebook.png"></a>
                    <a href="http://twitter.com/avaya">
                        <img src="images/social/twitter.png"></a>
                    <a href="http://www.linkedin.com/company/1494">
                        <img src="images/social/linkedin.png"></a>
                    <a href="http://www.youtube.com/Avayainteractive">
                        <img src="images/social/youtube.png"></a>
                    <a href="http://www.flickr.com/photos/avaya">
                        <img src="images/social/flickr.png"></a>
                    <a href="http://www.avaya.com/blogs/">
                        <img src="images/social/blog.png"></a>
                </div>
            </div>
        </div>

        <div class="container hidden-desktop">
            <div class="row" style="padding: 20px 0;">
                <div class="span6 info">
                    <table cellpadding="0" cellspacing="0" align="center">
                        
                    </table>
                </div>
                <div class="span6 info">
                   
                </div>
            </div>
        </div>

        <div class="bs-docs-social social-media">
            <div class="container">

                <div class="row  hidden-desktop">
                    <div class="span12">
                        <ul class="bs-docs-social-buttons">
                            <li><a href="https://www.facebook.com/avaya">
                                <img src="images/social/facebook-mobile.png"></a></li>
                            <li><a href="http://twitter.com/avaya">
                                <img src="images/social/twitter-mobile.png"></a></li>
                            <li><a href="http://www.linkedin.com/company/1494">
                                <img src="images/social/linkedin-mobile.png"></a></li>
                            <li><a href="http://www.youtube.com/Avayainteractive">
                                <img src="images/social/youtube-mobile.png"></a></li>
                            <li><a href="http://www.flickr.com/photos/avaya">
                                <img src="images/social/flickr-mobile.png"></a></li>
                            <li><a href="http://www.avaya.com/blogs/">
                                <img src="images/social/blog-mobile.png"></a></li>
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
                            <li class="muted">&copy; 2009-2014 Avaya Inc.</li>
                        </ul>
                    </div>
                </div>

            </div>
        </div>
    </footer>




    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    

    
   <%-- <script type="text/javascript" src="js/bootstrap-transition.js"></script>
    <script type="text/javascript" src="js/bootstrap-alert.js"></script>
    <script type="text/javascript" src="js/bootstrap-modal.js"></script>
    <script type="text/javascript" src="js/bootstrap-dropdown.js"></script>
    <script type="text/javascript" src="js/bootstrap-scrollspy.js"></script>
    <script type="text/javascript" src="js/bootstrap-tab.js"></script>
    <script type="text/javascript" src="js/bootstrap-tooltip.js"></script>
    <script type="text/javascript" src="js/bootstrap-popover.js"></script>
    <script type="text/javascript" src="js/bootstrap-button.js"></script>
    <script type="text/javascript" src="js/bootstrap-collapse.js"></script>
    <script type="text/javascript" src="js/bootstrap-carousel.js"></script>
    <script type="text/javascript" src="js/bootstrap-typeahead.js"></script>
    <script type="text/javascript" src="js/bootstrap-affix.js"></script>
    <script type="text/javascript" src="js/run_prettify.js"></script>--%>
    
    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->
</body>
</html>
