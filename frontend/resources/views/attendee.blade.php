<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="stylesheet" href="{{asset('css/attendeePage.css')}}">
    <script src="{{ asset('js/attendee.js') }}" defer></script>
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
                    <select id="dropdownbar" name="beer_type">
                        <option value="" disabled selected>-- Choose a beer --</option>
                        <option value="0">Pilsner</option>
                        <option value="1">Wheat</option>
                        <option value="2">IPA</option>
                        <option value="3">Staut</option>
                        <option value="4">Ale</option>
                        <option value="5">Alkoholfri</option>
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
                    <li>Rules For Ordering</li>
                    <li>Max order amount is 10 at a time</li>
                    <li>1</li>
                    <li>1</li>
                </ul>
            </div>
        </section>
    </div>
</body>
</html>