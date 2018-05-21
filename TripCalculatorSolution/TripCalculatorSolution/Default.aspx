<%@ Page Title="Home Page" Language="C#" AutoEventWireup="false" CodeBehind="Default.aspx.cs" Inherits="TripCalculatorSolution._Default" %>

    <html>
        <head>
            <meta charset="utf-8">
	        <meta http-equiv="X-UA-Compatible" content="IE=edge">
	        <meta name="viewport" content="width=device-width, initial-scale=1">

            <title>Basic Form</title>

            <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
            <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" integrity="sha384-WskhaSGFgHYWDcbwN70/dfYBj47jz9qbsMId/iRN3ewGhXQFZCSftd1LZCfmhktB" crossorigin="anonymous">
            <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js" integrity="sha384-smHYKdLADwkXOn1EmN1qk/HfnUcbVRZyYmZ4qpPea6sjB/pTJ0euyQp0Mk8ck+5T" crossorigin="anonymous"></script>

            <link rel="stylesheet" href="Content/assets/demo.css">
	        <link rel="stylesheet" href="Content/assets/form-basic.css">

        </head>

        <div class="main-content">
            <form class="form-basic" method="post" action="#">

                <div class="form-title-row">
                    <h1>Form Example</h1>
                </div>
                
                <div class="form-row">
                    <label>
                        <span>Full name</span>
                        <input type="text" name="name">
                    </label>
                </div>

                <div class="form-row">
                    <label>
                        <span>Email</span>
                        <input type="email" name="email">
                    </label>
                </div>

                <div class="form-row">
                    <label>
                        <span>Dropdown</span>
                        <select name="dropdown">
                            <option>Option One</option>
                            <option>Option Two</option>
                            <option>Option Three</option>
                            <option>Option Four</option>
                        </select>
                    </label>
                </div>

                <div class="form-row">
                    <label>
                        <span>Textarea</span>
                        <textarea name="textarea"></textarea>
                    </label>
                </div>

                <div class="form-row">
                    <label>
                        <span>Checkbox</span>
                        <input type="checkbox" name="checkbox" checked>
                    </label>
                </div>

                <div class="form-row">
                    <label><span>Radio</span></label>
                    <div class="form-radio-buttons">

                        <div>
                            <label>
                                <input type="radio" name="radio">
                                <span>Radio option 1</span>
                            </label>
                        </div>

                        <div>
                            <label>
                                <input type="radio" name="radio">
                                <span>Radio option 2</span>
                            </label>
                        </div>

                        <div>
                            <label>
                                <input type="radio" name="radio">
                                <span>Radio option 3</span>
                            </label>
                        </div>

                    </div>
                </div>

                <div class="form-row">
                    <button type="submit">Submit Form</button>
                </div>
            </form>
        </div>
    </html>