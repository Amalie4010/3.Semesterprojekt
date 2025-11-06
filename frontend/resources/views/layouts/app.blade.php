<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Beer</title>
    <link rel="stylesheet" href="{{ asset('css/style.css') }}">
</head>
<body>
    <header>
        <div class="topnav">
            <a href="{{ route('index') }}">
                <img src="{{ asset('img/BrewOS.png') }}" alt="Beer Logo" class="logo">
            </a>
        </div>
    </header>
    <main>
        <div class="container">    
            @yield('content')
        </div>
    </main>


<footer class="footer">
    <div class="footer-container">
        <div class="footer-column">
        <h3>Contact info</h3>
        <p>Syddansk Universitet<br>
            Campusvej 55<br>
            5230 Odense M
        </p>
        <p><strong>Phone:</strong> 12341234</p>
        <p><strong>Email:</strong> Random@ass.email.com</p>
    </div>
    <div class="footer-column">
        <h3>Links</h3>
        <ul>
            <li><a href="#">Privacy police</a></li>
            <li><a href="#">Teams of Service</a></li>
        </ul>
    </div>
    </div>
    <div class="footer-bottom">
       &copy; Group 3. All rights reserved.
    </div>
</footer>
