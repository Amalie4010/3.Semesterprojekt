<?php

namespace App\Http\Controllers;

use App\Models\Order;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;

class OrderController extends Controller
{
    // Post order to statistics microservice
    public function postOrder($order)
    {
        Http::post('/api/statistic/order', [
            'beer_type' => $order->beer_type,
            'quantity' => $order->quantity,
        ]);
    }

    // Create a new order function
    public function createOrder(Request $request)
    {
        //validating request data so every input from the form is correct!
        $data = $request->validate([
            'beer_type' => 'required|numeric',
            'quantity' => 'required|numeric|min:1|max:10'
        ]);

        //Here we make a new varaible and use a static method from Product model class and use the create() method to store $data from before into the database
        Order::create($data);

        /*  */
        $this->postOrder($data);



        /* Send response back with message in the header and data as data and with status code OBS: This isnt really required its just if anybody need it */
        /*
        return response()->json([
            'message' => 'Order saved successfully',
            'data' => $data
        ], 201);
        */
        return redirect(route('goto.attendee'))->with('success','Order Successful');

    }

    // Show all orders
    public function getOrders()
    {
        $data = Order::all();
    }
}
