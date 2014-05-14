<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lista_Traducciones.aspx.cs" Inherits="Lista_Traducciones" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
    <title>Translations Queue- Avaya</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="developer" content="William Ballesteros Blanco (wballesteros@avaya.com) - Avaya Inc. 2014">
    <link rel="icon" type="image/png" href="images/favicon.png" />
    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">

    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet">
    <link href="css/docs.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
  
    <link href="css/DT_bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-dialog.css" rel="stylesheet" type="text/css" />
    <link href="css/calendar.css" rel="stylesheet" type="text/css" />


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

        p.description {
            font-size: 0.8em;
            padding: 0 1em 1em;
            margin: 0;
        }

        #message {
            font-size: 0.7em;
            position: absolute;
            top: 1em;
            right: 1em;
            width: 350px;
            display: none;
            padding: 1em;
            background: #ffc;
            border: 1px solid #dda;
        }

        .btn-twitter {
			padding-left: 30px;
			background: rgba(0, 0, 0, 0) url(img/twitterIcon.png) -20px 6px no-repeat;
			background-position: -20px 11px !important;
		}
		.btn-twitter:hover {
			background-position:  -20px -18px !important;
		}
    </style>

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="assets/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="assets/ico/favicon.png">
</head>

<body>



    <!-- NAVBAR  ================================================== -->
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
                        <li style="width:300px;"><a href="sol_details.aspx" id="userName"></a></li>
                        <li><a href="#" id="exit">Log Out</a></li>
                    </ul>
                </div>

               
                
                <!--/.nav-collapse -->

            </div>
        </div>
    </div>


    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->

    <div class="container" id="detalles">
        <div class="row-fluid">
            <div class="span9">
                <hr style="margin-top: 0;">
                <div class="row-fluid">
                    <div class="span12">
                        <h2></h2><br/>
                        <div class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label" for="traductores">Select Translator:</label>
                                <div class="controls">
                                    <select id="traductores">
									</select>
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="Register">Accept</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



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

    <div class="container" id="calendario">
        <div class="row-fluid">
            <div class="span9">
                <hr style="margin-top: 0;">
                <div class="row-fluid">
                    <div class="span12">
                        <h2>Translations Queue Calendar</h2><br/>
                        <div class="page-header">

                            <div class="pull-right form-inline">
                                <div class="btn-group">
                                    <button class="btn btn-danger" data-calendar-nav="prev"><< Prev</button>
                                    <button class="btn" data-calendar-nav="today">Today</button>
                                    <button class="btn btn-danger" data-calendar-nav="next">Next >></button>
                                </div>
                                <div class="btn-group">
                                    <button class="btn btn-danger" data-calendar-view="year">Year</button>
                                    <button class="btn btn-danger active" data-calendar-view="month">Month</button>
                                    <button class="btn btn-danger" data-calendar-view="week">Week</button>
                                    <button class="btn btn-danger" data-calendar-view="day">Day</button>
                                </div>
                            </div>
                            <h3></h3>
                        </div>
                        <div class="row">
                            <div class="span9">
                                <div id="calendar"></div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.container -->


    <!-- Footer
    ================================================== -->
   <footer class="footer"><a href="http://www4.avaya.com/cala/SM_LATAM/Index.html" target="_blank"><img src="images/Translation-solcials_02.png"></a>
      

        <div class="bs-docs-social social-media">
            <div class="container">

                <div class="row  hidden-desktop">
                    <div class="span12">

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
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/prettyLoader.js"></script>
    <script type="text/javascript" src="js/ajaxfileupload.js"></script>

   
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>
  
    <script type="text/javascript" src="js/bootstrap-dialog.js"></script>

    <script type="text/javascript" src="js/mystyle.js"></script>
    <script type="text/javascript" src="js/holder/holder.js"></script>
    <script type="text/javascript" src="js/respond.src.js"></script>

    <script type="text/javascript" src="js/json2.js"></script>

    <script type="text/javascript" src="js/underscore-min.js"></script>
    <script type="text/javascript" src="js/jstz.min.js"></script>
    <script type="text/javascript" src="js/nl-NL.js"></script>
    <script type="text/javascript" src="js/calendar.js"></script>
    <script type="text/javascript" src="js/Lista_Traducciones.js"></script>

    

    <script type='text/javascript' src='js/jquery-migrate-1.2.1.js'></script>

    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->

   
</body>
</html>
