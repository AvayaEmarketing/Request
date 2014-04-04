<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trad_req_detail.aspx.cs" Inherits="trad_req_detail" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
    <title>Translation Request - Avaya</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="developer" content="William Ballesteros Blanco (wballesteros@avaya.com) - Avaya Inc. 2014">

    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="css/docs.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/messi.css" rel="stylesheet" />
    <link href="css/prettyLoader.css" rel="stylesheet" />
    <link href="css/DT_bootstrap.css" rel="stylesheet" type="text/css" />
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
                        <li><a href="traductor.aspx" id="my_solicits">My Translations</a></li>
                        <li><a href="#" id="r_details">Translation Details</a></li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Actions&nbsp;&nbsp;<b class="caret"></b></a>
                            <ul class="dropdown-menu" id="menu_actions">
                               <%-- <li><a href="#" id="s_Feedback">Send FeedBack</a></li>
                                <li><a href="#" id="s_Review">Send for Review</a></li>
                                <li><a href="#" id="s_Translate">Send Translate</a></li>--%>
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">My Profile&nbsp;&nbsp;<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="#" id="information">Information</a></li>
                                <li><a href="#" id="exit">Exit</a></li>
                                
                            </ul>
                        </li>
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
                        <h2>Translation Details</h2>

                        <div class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label" for="translation_name">Translation name:</label>
                                <div class="controls">
                                    <input id="translation_name" type="text" placeholder="Name" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="document_type">Document Type:</label>
                                <div class="controls">
                                    <input id="document_type" type="text" placeholder="Document Type" />
                                </div>
                            </div>

                            <div class="control-group" id="url" style="display: none;">
                                <label class="control-label" for="url_field">URL:</label>
                                <div class="controls">
                                    <input id="url_field" type="url" placeholder="URL" />
                                </div>
                            </div>

                            <div class="control-group" id="copy" style="display: none;">
                                <label class="control-label" for="copy_field">Copy:</label>
                                <div class="controls">
                                    <input id="copy_field" type="text" placeholder="Copy" />
                                </div>
                            </div>

                            <div class="control-group" id="file" style="display: none;">
                                <label class="control-label" for="copy_field">Original Document:</label>
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="Download">Download File</button>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="original_language">Original Language:</label>
                                <div class="controls">
                                    <input id="original_language" type="text" placeholder="Original language" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="translate_language">Translate Language:</label>
                                <div class="controls">
                                    <input id="translate_language" type="text" placeholder="Translate language" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="desired_date">Desired Date:</label>
                                <div class="controls">
                                    <input id="desired_date" type="text" placeholder="Desired Date" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="register_date">Registered Date:</label>
                                <div class="controls">
                                    <input id="register_date" type="text" placeholder="Register Date" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="prioridad">Priority:</label>
                                <div class="controls">
                                    <input id="prioridad" type="text" placeholder="Priority" />
                                </div>
                            </div>

                            <div class="control-group" id="priority" style="display: none;">
                                <label class="control-label" for="priority_comment">Priority Comment:</label>
                                <div class="controls">
                                    <textarea id="priority_comment" rows="3"></textarea>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="observations">Observations:</label>
                                <div class="controls">
                                    <textarea id="observations" rows="3"></textarea>
                                </div>
                            </div>

                            <h2 id="msg_correction" style="display: none;">Correction Details</h2>

                             <div class="control-group" id="url_c" style="display: none;">
                                <label class="control-label" for="url_field">URL:</label>
                                <div class="controls">
                                    <input id="url_c1" type="url" placeholder="URL" />
                                </div>
                            </div>

                            <div class="control-group" id="copy_c" style="display: none;">
                                <label class="control-label" for="copy_field">Copy:</label>
                                <div class="controls">
                                    <input id="copy_c1" type="text" placeholder="Copy" />
                                </div>
                            </div>

                            <div class="control-group" id="file_c" style="display: none;">
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="file_c1">Download File</button>
                                </div>
                            </div>

                            <div class="control-group" id="obs_c" style="display: none;">
                                <label class="control-label" for="observations">Observations:</label>
                                <div class="controls">
                                    <textarea id="obs_c1" rows="3"></textarea>
                                </div>
                            </div>
           
                        </div>


                    </div>
                </div>


            </div>
        </div>
    </div>

    <div class="container" id="responder" style="display: none;">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">

                <div class="row-fluid">
                    <div class="span12">
                        <h2>Translation Details</h2>

                        <table class="table table-striped table-bordered dataTable" style="width: 100%; text-align: center; visibility: visible;" border="0" cellspacing="0" cellpadding="0">
                            <thead id="thead">
                                <tr role="row">
                                    <th>Translation Name</th>
                                    <th>State</th>
                                    <th>Original Language</th>
                                    <th>Translate Language</th>
                                    <th>Register Date</th>
                                    <th>Desired Date</th>
                                    <th>Priority</th>
                                    
                                </tr>
                            </thead>

                            <tbody id="tbody">
                                
                               
                            </tbody>
                        </table>
                        <br/><br/>
                        <h2>FeedBack Information</h2>

                        <div class="control-group">
                                <label class="control-label" for="estimated_date"><span style="color: #cc0000;">*</span>Estimated Date:</label>
                                <div id="datetimepicker3" class="controls">
                                    <input data-format="dd/MM/yyyy" type="text" id="estimated_date" />
                                    <span class="add-on">
                                        <i data-date-icon="icon-calendar"></i>
                                    </span>
                                </div>
                            </div>

                           <div class="control-group">
                                <label class="control-label" for="priority_comment"><span style="color: #cc0000;">*</span>Observations:</label>
                                <div class="controls">
                                    <textarea id="observations_feedback" rows="3"></textarea>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="estado_feed"><span style="color: #cc0000;">*</span>Request State:</label>
                                <div class="controls">
                                    <select id="estado_feed">
                                        <option value="" selected="selected"></option>
                                        <option value="2" >Aceptada</option>
                                        <option value="3">En espera</option>
                                    </select>
                                </div>
                            </div>

                           <div class="control-group">
                                <label class="control-label" for="revision"><span style="color: #cc0000;">*</span>Requires a review?:</label>
                                <div class="controls">
                                    <select id="revision">
                                        <option value="" selected="selected"></option>
                                        <option value="YES" >YES</option>
                                        <option value="NO">NO</option>
                                    </select>
                                </div>
                            </div>

                            <div class="control-group">
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="send_feedback">Send</button>
                                </div>
                            </div>
                           
                    </div>
                </div>


            </div>
        </div>
    </div>

    <!--para mandar al review tiene que haber enviado al feedback al solicitante-->
    <div class="container" id="review" style="display: none;">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">

                <div class="row-fluid">
                    <div class="span12">
                        <h2>Translation Details</h2>

                        <table class="table table-striped table-bordered dataTable" style="width: 100%; text-align: center; visibility: visible;" border="0" cellspacing="0" cellpadding="0">
                            <thead id="thead1">
                                <tr role="row">
                                    <th>Translation Name</th>
                                    <th>State</th>
                                    <th>Original Language</th>
                                    <th>Translate Language</th>
                                    <th>Register Date</th>
                                    <th>Desired Date</th>
                                    <th>Priority</th>
                                    
                                </tr>
                            </thead>

                            <tbody id="tbody1">
                               
                            </tbody>
                        </table>
                        <br/><br/>
                        <h2>Send for Review</h2>

                            <div class="control-group">
                                <label class="control-label" for="document_type"><span style="color: #cc0000;">*</span>Choose how you send the translate:</label>
                                <div class="controls">
                                    <select id="Select1">
                                        <option value="" selected="selected"></option>
                                        <option value="1">Text</option>
                                        <option value="2">Document</option>
                                    </select>
                                </div>
                            </div>

                            <div class="control-group" id="copy_r" style="display:none;">
                                <label class="control-label" for="copy_field">Type or Paste your Translation Text:</label>
                                <div class="controls">
                                    <textarea id="copy_field_r" rows="3"></textarea>
                                </div>
                            </div>

                            <div class="control-group" id="file_r" style="display:none;">
                                <label class="control-label" for="file_field">Upload Translation File:</label>
                                <div class="controls">
                                    <span class="btn btn-default btn-file"> Browse <input id="fileToUpload" type="file"  name="fileToUpload" /> </span>
                                </div>
                            </div>
                        

                           <div class="control-group">
                                <label class="control-label" for="observations_r"><span style="color: #cc0000;">*</span>Observations for Review:</label>
                                <div class="controls">
                                    <textarea id="observations_r" rows="3"></textarea>
                                </div>
                            </div>

                            <div class="control-group">
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="Register_r">Send</button>
                                </div>
                            </div>
                           
                    </div>
                </div>


            </div>
        </div>
    </div>
    <!-- /.container -->

    <!--para mandar la traduccion tiene que haber recibido la respuesta del revisor en caso de ser necesaria-->
    <div class="container" id="translate" style="display: none;">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">

                <div class="row-fluid">
                    <div class="span12">
                        <h2>Translation Details</h2>

                        <table class="table table-striped table-bordered dataTable" style="width: 100%; text-align: center; visibility: visible;" border="0" cellspacing="0" cellpadding="0">
                            <thead id="thead2">
                                <tr role="row">
                                    <th>Translation Name</th>
                                    <th>State</th>
                                    <th>Original Language</th>
                                    <th>Translate Language</th>
                                    <th>Register Date</th>
                                    <th>Desired Date</th>
                                    <th>Priority</th>
                                    
                                </tr>
                            </thead>

                            <tbody id="tbody2">
                               
                            </tbody>
                        </table>
                        <br/><br/>
                        <h2>Send Translate</h2>

                            <div class="control-group">
                                <label class="control-label" for="document_type"><span style="color: #cc0000;">*</span>Choose how you send the translate:</label>
                                <div class="controls">
                                    <select id="Select2">
                                        <option value="" selected="selected"></option>
                                        <option value="1">Text</option>
                                        <option value="2">Document</option>
                                    </select>
                                </div>
                            </div>

                            <div class="control-group" id="copy_r2" style="display:none;">
                                <label class="control-label" for="copy_field">Type or Paste your Translation Text:</label>
                                <div class="controls">
                                    <textarea id="copy_field_t" rows="3"></textarea>
                                </div>
                            </div>

                            <div class="control-group" id="file_r2" style="display:none;">
                                <label class="control-label" for="file_field">Upload Translation File:</label>
                                <div class="controls">
                                    <span class="btn btn-default btn-file"> Browse <input id="fileToUpload2" type="file"  name="fileToUpload" /> </span>
                                </div>
                            </div>
                        

                           <div class="control-group">
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="Register_t">Send</button>
                                </div>
                            </div>
                           
                    </div>
                </div>


            </div>
        </div>
    </div>
    <!-- /.container -->


   <!--para mandar la traduccion tiene que haber recibido la respuesta del revisor en caso de ser necesaria-->
    <div class="container" id="data_review" style="display: none;">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">

                <div class="row-fluid">
                    <div class="span12">
                        <h2>Translation Details</h2>

                        <table class="table table-striped table-bordered dataTable" style="width: 100%; text-align: center; visibility: visible;" border="0" cellspacing="0" cellpadding="0">
                            <thead id="thead3">
                                <tr role="row">
                                    <th>Translation Name</th>
                                    <th>State</th>
                                    <th>Original Language</th>
                                    <th>Translate Language</th>
                                    <th>Register Date</th>
                                    <th>Desired Date</th>
                                    <th>Priority</th>
                                    
                                </tr>
                            </thead>

                            <tbody id="tbody3">
                               
                            </tbody>
                        </table>
                        <br/><br/>
                        <h2>View Review</h2>

                            <div class="control-group" id="text_t" style="display: none;">
                                <label class="control-label" for="copy_field">Review Text:</label>
                                <div class="controls">
                                    <input id="texto_rw" type="text" readonly="readonly"/>
                                </div>
                            </div>

                            <div class="control-group" id="doc_t" style="display: none;">
                                <label class="control-label" for="copy_field">Review File:</label>
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="BtnDownload_rw">Download Review File</button>
                                </div>
                            </div>
                            
                            <div class="control-group">
                                <label class="control-label" for="priority_comment">Review Observations:</label>
                                <div class="controls">
                                    <textarea id="observations_rw" rows="3" readonly="readonly"></textarea>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="estimated_date"><span style="color: #cc0000;">Review Date:</span></label>
                                <div class="controls">
                                    <input data-format="dd/MM/yyyy" type="text" id="date_rw" readonly="readonly" />
                                </div>
                            </div>

                           
                           <h3><span style="color: #cc0000;">To close the Review, Please click in Actions -> Close Review</span></h3>
                           
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
                    <!--<img style="float: none; padding-bottom: 8px" src="assets/img/footer/mail.png">
        <a href="mailto:conferenceHeadquarters@madisonpg.com">ConferenceHeadquarters@madisonpg.com</a>-->
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
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>
    <script type="text/javascript" src="js/bootstrap-transition.js"></script>
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
    <script type="text/javascript" src="js/run_prettify.js"></script>
    <script type="text/javascript" src="js/bootstrap-dialog.js"></script>
    <script type="text/javascript" src="js/mystyle.js"></script>
    <script type="text/javascript" src="js/holder/holder.js"></script>
    <script type="text/javascript" src="js/respond.src.js"></script>
    <script type="text/javascript" src="js/prettyLoader.js"></script>
    <script type="text/javascript" src="js/ajaxfileupload.js"></script>
    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/DT_bootstrap.js"></script>
    <script type="text/javascript" src="js/trad_req_detail.js"></script>
    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->
</body>
</html>
