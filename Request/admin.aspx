<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Avaya Translate Request - Login</title>
    
    <link rel="icon" type="image/png" href="images/favicon.png" />
    <link href="css/admin.css" rel="stylesheet"/>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/prettyLoader.css" rel="stylesheet" />
    <link href="css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/docs.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/bootstrap-dialog.css" rel="stylesheet" />


    
</head>
<body>
<div id="emarketing"><img src="images/logos-EA-Avaya.png"></div>

<div id="loginBox"> 
  
<br>
<br>

  <div id="loginForm" >
  
    <fieldset id="body">
              <fieldset id="fieldstemp2">
                <label for="email">User or Email</label>
                    <input type="text" name="usuario" id="usuario" />
        </fieldset>
                <fieldset id="Fieldset1">
                    <label for="email">Password</label>
                </fieldset>
                    <input type="password" name="UserPass" id="UserPass" />
                <input type="image" src="images/btn.png" id="login" value="" width="110" height="50" />
                <!-- <label for="checkbox"><input type="checkbox" id="checkbox" />Recuerdame</label>  -->
    </fieldset>
            <span><a href="#" id="olvido"></a></span>
  </div>
</div> 
<div class="logo-req">
 <img src="images/logo-request.png"> </div> 
<div class="personas">
<img src="images/personas.png"> </div> 

<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/json2.js"></script>
<script type="text/javascript" src="js/prettyLoader.js"></script>
<script type="text/javascript" src="js/jquery.sha256.js"></script>
<script src="js/bootstrap.min.js"></script><script src="js/bootstrap-dialog.js"></script>
<script type="text/javascript" src="js/LoginAjax.js"></script>
</body>
</html>
