<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="stylesheet" href="{{asset('css/attendeePage.css')}}">
</head>
<body>
    <h1>Order your beer</h1>
    <div id="Container">
        @if(session()->has('success'))
            <div>
                {{session('success')}}
            </div>
        @endif
        <section class="section" id="leftside">
            <form id="formSubmitBeer" action="{{route('order.makeOrder')}}" method="post" >
            @csrf
                <div id="chooseBeer">
                    <label for=""></label>
                    <!-- dropdownbar -->
                    <!-- the beers are hardcoded into the code, should have made it different, havnt improved cuz of time -->
                    <select id="dropdownbar" name="beer_type">
                        <option disabled selected>-- Choose a beer --</option>
                        @foreach ($beertype as $beerType)
                            <option value="{{ $beerType->type_id}}">{{$beerType->name}}</option>
                        @endforeach
                    </select>
                </div>
                <div>
                    <input name="quantity" type="number" max="10" min="1"  >
                    <input class="button" type="submit" value="Place your order">
                </div>
                </form>
        </section>


        <section class="section" id="rightside" >
            <div id="ruleset">
                <ul>
                    <p>Rules For Ordering</p>
                    <li>Max order amount is 10 at a time</li>
                </ul>
            </div>
        </section>
    </div>
</body>
</html>