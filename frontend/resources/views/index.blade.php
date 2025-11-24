@extends('layouts.app')

@section('content')
  <div class="container">
    <div class="section">
      <p>Attendee Landpage</p>
      <a href="{{ route('goto.attendee')}}" >
      <button>Continue as Attendee</button>
      </a>
    </div>
    <div class="section">
      <p>Operator Landpage</p>
      <a href="{{ route('goto.login')}}" >
      <button>Continue as Operator</button>
      </a>
    </div>
  </div>
@endsection
