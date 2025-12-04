<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

use App\Models\Beer;



class ViewController extends Controller
{
    //


    public function attendee()
    {
    $beertypes = Beer::All();
    return view('attendee',['beertype'=>$beertypes]);
    }

}
