<%@ Page Language="C#" AutoEventWireup="true" CodeFile="solicitante.aspx.cs" Inherits="solicitante" %>

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
    
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet">
    <link href="css/docs.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    
    
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
    <script type="text/javascript" src="js/solicitante.js"></script>

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
                        <li><a href="#" id="new_solicit">New Request</a></li>
                        <li><a href="#" id="my_solicits">My Requests</a></li>
                        <li><a href="#" id="cola_traducciones">Translations Queue</a></li>
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

    <div class="container" id="nueva_solicitud" style="display: none;">
        <div class="row-fluid">
            <div class="span9">
                <hr style="margin-top: 0;">
                <div class="row-fluid">
                    <div class="span12">
                        <h2>New Request</h2>

                        <div class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label" for="translation_name"><span style="color: #cc0000;">*</span>Translation Name:</label>
                                <div class="controls">
                                    <input id="translation_name" type="text" placeholder="Name" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="document_type"><span style="color: #cc0000;">*</span>Document Type:</label>
                                <div class="controls">
                                    <select id="document_type">
                                        <option value="" selected="selected"></option>
                                        <option value="1" >Copy (Max 300 characters)</option>
                                        <option value="3">Text Document (PDF,Word,txt)</option>
                                    </select>
                                </div>
                            </div>

                            <div class="control-group" id="url" style="display:none;">
                                <label class="control-label" for="url_field">Type or Paste URL:</label>
                                <div class="controls">
                                    <input id="url_field" type="url" placeholder="URL" />
                                </div>
                            </div>

                            <div class="control-group" id="copy" style="display:none;">
                                <label class="control-label" for="copy_field">Type or Paste your Copy:</label>
                                <div class="controls">
                                    <textarea id="copy_field" rows="7" onkeyup="countChar(this)" ></textarea>
                                    <div id="charCount"></div>
                                </div>
                            </div>

                            <div class="control-group" id="file" style="display:none;">
                                <label class="control-label" for="file_field">Upload your File:</label>
                                <div class="controls">
                                    <span class="btn btn-danger btn-file"  > Browse <input id="fileToUpload" type="file" name="fileToUpload"  /></span> Max File Size: 10MB 
                                </div>
                            </div>

                            <div class="control-group" id="doc_content" style="display:none;">
                                <label class="control-label" for="doc_content_text">File:</label>
                                <div class="controls">
                                    <label id="name_document" class="control-label"></label>
                                    <span id="results"></span>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="observations"><span style="color: #cc0000;">*</span>Observations:</label>
                                <div class="controls">
                                    <textarea id="observations" rows="7" placeholder="put here all your observations"></textarea>
                                </div>
                            </div>


                            <div class="control-group">
                                <label class="control-label" for="original_language"><span style="color: #cc0000;">*</span>Original Language:</label>
                                <div class="controls">
                                    <select id="original_language">
                                        <option value="" selected="selected"></option>
                                        <option value="Spanish">Spanish</option>
                                        <option value="English">English</option>
                                        <option value="Portuguese">Portuguese</option>
                                    </select>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="translate_language"><span style="color:#cc0000;">*</span>Desired Language(s):</label>
                                <div id="translate_language" class="controls">
                                    <label class="checkbox">
                                      <input type="checkbox" name="translate"  value="Spanish">Spanish
                                    </label>
                                    <label class="checkbox">
                                      <input type="checkbox" name="translate"  value="English">English
                                    </label>
                                    <label class="checkbox">
                                      <input type="checkbox" name="translate"  value="Portuguese">Portuguese
                                    </label>
                                    
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="desired_date"><span style="color: #cc0000;">*</span>Desired Date:</label>
                                <div id="datetimepicker3" class="controls">
                                    <input data-format="dd/MM/yyyy" type="text" id="desired_date" readonly="readonly" />
                                    <span class="add-on">
                                        <i data-date-icon="icon-calendar"></i>
                                    </span>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="prioridad"><span style="color: #cc0000;">*</span>Priority:</label>
                                <div class="controls">
                                    <select id="prioridad">
                                        <option value="" selected="selected"></option>
                                        <option value="Low" >Low</option>
                                        <option value="Medium">Medium</option>
                                        <option value="High">High</option>

                                    </select>
                                </div>
                            </div>

                            <div class="control-group" id="priority" style="display:none;">
                                <label class="control-label" for="priority_comment">Reason for the priority:</label>
                                <div class="controls">
                                    <textarea id="priority_comment" rows="7"></textarea>
                                </div>
                            </div>
                            <div class="control-group">
                                <div class="controls">
                                    <button style="top: 0 !important;" type="submit" class="btn btn-danger" id="Register">Send</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container" id="dt_my_solicits">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">
                
                

                <div class="row-fluid">
                    <div class="span12">
                        <h2>My Requests</h2>
                        
        

        <table id="datatables" cellpadding="0" cellspacing="0" border="0" style="width: 100%; text-align: center; visibility: hidden" class="table table-striped table-bordered">
            <thead id="thead">
                <tr>
                    
                    
                    <th class="sorting" width="15%">Translation Name</th>
                    <th class="sorting" width="10%">State</th>
                    <th class="sorting" width="12%">Original Lang</th>
                    <th class="sorting" width="12%">Translate Lang</th>
                    <th class="sorting" width="14%">Date of Requirement</th>
                    <th class="sorting" width="14%">Desired Date</th>
                    <th class="sorting" width="14%">Estimated Date</th>
                    <th class="sorting" width="8%">Priority</th>
                    <th class="sorting" width="7%">Details</th>

                    
                </tr>
            </thead>
            <tbody id="tbody">
            </tbody>
            <tfoot>
                <tr>
                    <td>
                       <div id="toExcel"><a href="#" id="btnDescargaExcel">
                           <img src="images/xls.png" alt="to Excel" /></a>
                       </div>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    
                    
                </tr>
            </tfoot>
        </table>
                         </div></div></div></div>
    </div>
    <!-- /.container -->

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
    
    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->
</body>
</html>
