<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Order extends Model
{
    /* this is a model that reprensate the resource in the db so the data can be manipulated in the code base */
    protected $fillable = ['beer_type', 'quantity'];
}
