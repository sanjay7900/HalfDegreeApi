$(document).ready(function () {

    function LoginFormGenrate() {
        $("#main").empty();
        $("#main").append('<br><br><br><center><span id="message" class="text-danger mt-4 fs-1 shadow-lg  rounded color-danger"> </span></center><br><br>\
                             <div width="50%" id="loginForm" class="container text-light col-4 border shadow-lg p-3 mb-5 bg-body rounded"> \
                                <center><h1>Login Form<h1></center><hr>\
                                  <br><hr>\
                                <input type="Email" id="emaillogin" class="form-control" required="requierd" name="Email"><br><hr>\
                                <input type="password"  class="form-control" id="password" required name="password"><br><hr>\
                                <button  name="submit" id="LoginButton" class="btn btn-lg d-grid gap-2 btn-primary" value="Login"> Login</button>\
                                <br> Not Registre  <button id="NotRegistred"> Click Here</button> </div>'


        );
    }

    function SignFormGenrate() {
        $("#main").empty();
        $("#main").append('<br><br><br><div width="50%" mt-4 id="loginForm" class="container text-light mt-4 col-6 row-cols-sm-auto shadow-lg p-3  bg-body rounded box-shadow mb-3"> \
                <center><h1 class="mt-4 p-4 ">SignUp Form<h1></center><hr>\
               <br><hr>\
                <input type="text"  class="form-control" required="requierd" name="Name"><br><hr>\
                <input type="Email"  class="form-control" required name="Email"><br><hr>\
                <input type="password"  class="form-control" required name="Password"><br><hr>\
                <select name="Gender" class="form-control user-select-none" required>select-\
                  <option>Male</option>\
                  <option>FeMale</option>\
                  <option>TransGender</option>\
                </select><br><hr>\
                <input type="textarea"  class="form-control" required name="Address"><br><hr>\
                <input type="submit" name="submit" id="SignUpButton" class="btn btn-primary btn-lg" value="Sign Up">\
                <br> Already have an Account <button id="AlreadyRegistered"> Click Here</button><div>'


        );
    }
    $("#login").click(function () {
        LoginFormGenrate();
    })
    $("#SignUp").click(function () {
        SignFormGenrate();
    })

    $("#main").on("click", "button#AlreadyRegistered", function () {
        LoginFormGenrate();

    });
    $("#main").on("click", "button#NotRegistred", function () {

        var v = $("input#emaillogin").val();
        console.log(v);
        SignFormGenrate();
    });

    $("#main").on("click", "button#LoginButton", function () {
        var email =$("input#emaillogin").val();
        var password = $("input#password").val();
        console.log(email, password);
        $.ajax({
            url: "https://localhost:7167/Home/LoginUser?Emailid="+email+"&password="+password,
            type: "GET",
            
            //contentType: "application/json",
            //dataType: 'json',
            success: function (data) {
                if (data.result == "Redirect") {
                    window.location = data.url;
                }

            },
            error: function (error) {
               
                $("#message").text("Invalid email password ");
            }
        })

        
    });
    function LoginAjax(email, password) {

    }
















});