<?php

namespace App\Http\Controllers;

use App\Models\Order;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use App\Models\Event;

class OrderController extends Controller
{
    // Post order to statistics microservice
    public function postOrder($order)
    {
        Http::post('http://127.0.0.1:8080/api/statistics/order', [
            'beer_type' => $order->type_id,
            'quantity' => $order->quantity,
            'createdAt' => $order->created_at,
        ]);
    }

    // Create a new order function
    public function createOrder(Request $request)
    {
        //validating request data so every input from the form is correct!
        $data = $request->validate([
            'type_id' => 'required|numeric',
            'quantity' => 'required|numeric|min:1|max:10'
        ]);

        //Here we make a new varaible and use a static method from Product model class and use the create() method to store $data from before into the database
        //$order = Order::create($data);

        /* we will not be using the normal way to save / create a row into the database
            because we will be making use of a method which cant be inside the validate part, 
            so we do it a bit differently
        */
        // this will get the latest id from the Event table, and save it into the variable.
        $latestEventId = Event::latest()->first()->id;

        // new instance of the "Order" model
        $order = new Order();

        // the validate part columns / parts are saved into the order instance here
        $order->type_id = $data['type_id'];
        $order->quantity = $data['quantity'];

        // the method we used above to save the newest id into the varible, set into the event_id column
        $order->event_id = $latestEventId;

        // the order is saved into the database
        $order->save();
        //should work not tested yet.

        /* Use post order to send to statistic */
        $this->postOrder($order);

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
