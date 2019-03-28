<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LocalTheatre._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>    <style>
  .carousel-inner > .item > img,
  .carousel-inner > .item > a > img {
      width: 70%;
      margin: auto;
  }
  </style>
    <div class="jumbotron">
        <h1>Local Theatre</h1>
        <p class="lead">Visit us today!</p>
        <h4> Buy some overpriced alcohol!</h4>
    </div>

    <div class="row">
       <div id="myCarousel" class="carousel slide" data-ride="carousel">
    <!-- Carousel indicators -->
    <ol class="carousel-indicators">
        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
        <li data-target="#myCarousel" data-slide-to="1"></li>
        <li data-target="#myCarousel" data-slide-to="2"></li>
    </ol>   
    <!-- Wrapper for carousel items -->
    <div class="carousel-inner">
        <div class="item active">
            <img src="images/logo.jpg" alt="First Slide">
        </div>
        <div class="item">
            <img src="images/image2.jpg" alt="Second Slide">
        </div>
        <div class="item">
            <img src="images/image3.jpeg" alt="Third Slide">
        </div>
    </div>
    <!-- Carousel controls -->
    <a class="carousel-control left" href="#myCarousel" data-slide="prev">
        <span class="glyphicon glyphicon-chevron-left"></span>
    </a>
    <a class="carousel-control right" href="#myCarousel" data-slide="next">
        <span class="glyphicon glyphicon-chevron-right"></span>
    </a>
</div>
    </div>
</div>
    <div class="jumbotron">
        <h1>Theatre Blog</h1>
        <p class="lead">Read our posts! Register! Leave us a comment!</p>
       
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
             Let us know how you found the page!
            </p>
            <p>
               
            </p>
        </div>
        <div class="col-md-4">
         
        </div>
 


</asp:Content>
