<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rev_req_detail.aspx.cs" Inherits="rev_req_detail" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
    <title>Translation Request - Avaya</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="developer" content="William Ballesteros Blanco (wballesteros@avaya.com) - Avaya Inc. 2014">
    <link rel="icon" type="image/png" href="images/favicon.png" />
    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="css/docs.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    
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
                        <li><a href="revisor.aspx" id="my_solicits">My Reviews</a></li>
                        <li><a href="#" id="r_details">Review Details</a></li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" id="actions">Actions&nbsp;&nbsp;<b class="caret"></b></a>
                            <ul class="dropdown-menu" id="menu_actions">
                                <li><a href="#">No Actions Yet..</a></li>
                            </ul>
                        </li>
                        <li style="width:300px;"><a href="rev_details.aspx" id="userName"></a></li>
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
                        <h2>Request Details</h2>

                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="estado">Resquest state:</label>
                                <div class="controls">
                                    <span id="estado"></span>
                                </div>
                            </div>


                            <div class="control-group">
                                <label class="control-label" for="udnombre">Requester name:</label>
                                <div class="controls">
                                    <input id="udnombre" type="text"  readonly="readonly"/>
                                </div>
                            </div>

                            <div class="control-group" id="url" style="display: none;">
                                <label class="control-label" for="url_field">URL:</label>
                                <div class="controls">
                                    <input id="url_field" type="url" placeholder="URL" readonly="readonly"/>
                                </div>
                            </div>

                            <div class="control-group" id="copy" style="display: none;">
                                <label class="control-label" for="copy_field">Copy:</label>
                                <div class="controls">
                                    <input id="copy_field" type="text" placeholder="Copy" readonly="readonly"/>
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
                                    <input id="original_language" type="text" placeholder="Original language" readonly="readonly"/>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="translate_language">Translate Language:</label>
                                <div class="controls">
                                    <input id="translate_language" type="text" placeholder="Translate language" readonly="readonly"/>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="prioridad">Priority:</label>
                                <div class="controls">
                                    <input id="prioridad" type="text" placeholder="Priority" readonly="readonly" />
                                </div>
                            </div>

                            <div class="control-group" id="priority" style="display: none;">
                                <label class="control-label" for="priority_comment">Priority Comment:</label>
                                <div class="controls">
                                    <textarea id="priority_comment" rows="3" readonly="readonly"></textarea>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="observations">Observations:</label>
                                <div class="controls">
                                    <textarea id="observations" rows="3" readonly="readonly"></textarea>
                                </div>
                            </div>

                            <h2>Translate Details</h2>
                            <div class="control-group">
                                <label class="control-label" for="delivery_date">Delivery Date (Estimated):</label>
                                <div class="controls">
                                    <input id="delivery_date" type="text" readonly="readonly" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="desired_date">Desired Date:</label>
                                <div class="controls">
                                    <input id="desired_date" type="text" readonly="readonly" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="revision_date">Send to Review Date:</label>
                                <div class="controls">
                                    <input id="revision_date" type="text" readonly="readonly" />
                                </div>
                            </div>

                            <div class="control-group" id="text_t" style="display: none;">
                                <label class="control-label" for="copy_field">Translation (Text):</label>
                                <div class="controls">
                                    <textarea id="texto_t" rows="3" readonly="readonly"></textarea>
                                </div>
                            </div>

                            <div class="control-group" id="doc_t" style="display: none;">
                                <label class="control-label" for="copy_field">Translation (Document):</label>
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="BtnDownload">Download File</button>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="TR_observations">Observations to Review:</label>
                                <div class="controls">
                                    <textarea id="TR_observations" rows="3" readonly="readonly"></textarea>
                                </div>
                            </div>




                        </div>
                    </div>


                </div>
            </div>


        </div>
    </div>

    <!--send feedback -->
    <div class="container" id="send_correction" style="display: none;">
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
                            <tbody id="tbody1"></tbody>
                        </table>
                        <br />
                        <br />
                        <h2>Send Review</h2>

                            <div class="control-group">
                                <label class="control-label" for="document_type"><span style="color: #cc0000;">*</span>Choose how you send the review:</label>
                                <div class="controls">
                                    <select id="Select1">
                                        <option value="" selected="selected"></option>
                                        <option value="1">Text</option>
                                        <option value="2">Document</option>
                                    </select>
                                </div>
                            </div>

                            <div class="control-group" id="copy_r" style="display:none;">
                                <label class="control-label" for="copy_field">Type or Paste your Review Text:</label>
                                <div class="controls">
                                    <textarea id="copy_field_r" rows="3"></textarea>
                                </div>
                            </div>

                            <div class="control-group" id="file_r" style="display:none;">
                                <label class="control-label" for="file_field">Upload Review File:</label>
                                <div class="controls">
                                    <span class="btn btn-default btn-file"> Browse <input id="fileToUpload" type="file"  name="fileToUpload" /> </span>
                                    Max File Size: 10MB 
                                </div>
                            </div>

                            <div class="control-group" id="doc_content" style="display:none;">
                                <label class="control-label" for="doc_content_text">File:</label>
                                <div class="controls">
                                    <label id="name_document" class="control-label"></label>
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
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>
  
    <script type="text/javascript" src="js/bootstrap-dialog.js"></script>
    <script type="text/javascript" src="js/mystyle.js"></script>
    <script type="text/javascript" src="js/holder/holder.js"></script>
    <script type="text/javascript" src="js/respond.src.js"></script>
    <script type="text/javascript" src="js/prettyLoader.js"></script>
    <script type="text/javascript" src="js/ajaxfileupload.js"></script>
    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/DT_bootstrap.js"></script>
    <script type="text/javascript" src="js/rev_req_detail.js"></script>
    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->

 
</body>
</html>
