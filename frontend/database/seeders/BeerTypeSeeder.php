<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class BeerTypeSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        // these are the types of beer we sell, they will be shown in the attendee page
        DB::table('beertype')->insert([
            [
                'type_id'=>0,
                'name'=>'Pilsner'
            ],
            [
                'type_id'=>1,
                'name'=>'Wheat'  
            ],
            [
                'type_id'=>2,
                'name'=>'IPA'
            ],
            [
                'type_id'=>3,
                'name'=>'Staut'
            ],
            [
                'type_id'=>4,
                'name'=>'Ale'
            ],
            [
                'type_id'=>5,
                'name'=>'Alkoholfri'
            ]
        ]);
        /* now what i gotta do is make it so the dropdownbox shows foreach item in the table.
            and when you make an order it will send the "type_id", instead of the hardcoded value inside of the 
        */
    }
}
