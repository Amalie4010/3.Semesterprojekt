<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="stylesheet" href="{{asset('css/authenticationpage.css')}}">
</head>
<body>
    <main id="container">
        <h1 class="text">Log In</h1>
        <form method="post" action="{{route('login.attempt')}}">
        @csrf
        @if($errors->any())
            <div>
                <ul>
                @foreach($errors->all() as $error)
                    <li>{{$error}}</li>
                @endforeach
                </ul>
            </div>
        @endif
        <input type="email" name="email" autocomplete="something@gmail.com" placeholder="Email" >
        <br>
        <input type="password" name="password" placeholder="Password" >
        <br>
        <button type="submit">Login</button>
        </form>
    </main>
</body>
</html>