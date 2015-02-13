package Analytics;
use Dancer ':syntax';
use Dancer::Plugin::Ajax;
use Time::Local;

our $VERSION = '0.1';

get '/' => sub {
    template 'index';
};

get '/analytics' => sub {
    template 'analytics';
};

my $Monthly = [
  [ 356, 23, 15.5, "Max"    ],
  [ 359, 20, 18.0, "Max"    ],
  [ 358, 22, 16.3, "Paul"   ],
  [ 360, 23, 15.7, "Jim"    ],
  [ 347, 22, 15.8, "Eddie"  ],
  [ 327, 22, 14.9, "Max"    ],
  [ 331, 24, 13.8, "Max"    ],
  [ 342, 22, 15.5, "Ryan"   ],
  [ 359, 23, 15.6, "Max"    ],
  [ 353, 23, 15.3, "Max"    ],
  [ 352, 21, 16.8, "Elliot" ],
  [ 322, 24, 13.4, "Max"    ],
];

ajax "/chart_data/:user_id/:measure_id" => sub {
    # no_cache;
    # debug "Returning chart data from [" . params->{user_id} . "]["
                                        # . params->{measure_id} . "]";
    # my $data = AM::User->chart_data(params->{user_id}, params->{measure_id});
    # my $data = [[ 10, 10, 1, "abc", 10, "ABC" ]];
    my $data = [[[ 10, 20 ], [ 20, 15 ], [ 30, 25 ]]];
    my $data = [[
        map [ timegm(0, 0, 0, 1, $_, 2014) * 1000, $Monthly->[$_][2] ],
            0 .. $#$Monthly
    # ],[
        # map [ timegm(0, 0, 0, 1, $_, 2014) * 1000, $Monthly->[$_][0] ],
            # 0 .. $#$Monthly
    ]];
    # Don't use Data::Dumper to print out $data - it will turn some numbers
    # into strings which to_json will quote and which will make jqplot unhappy.
    # Data::Printer is OK.
    # debug p $data;
    to_json($data)
};

true;
