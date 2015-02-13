package Analytics;
use Dancer ':syntax';
use Dancer::Plugin::Ajax;
use Time::Local;

our $VERSION = '0.1';

get '/' => sub {
    template 'index';
};

get '/monthly' => sub {
    template 'monthly';
};

get '/daily' => sub {
    template 'daily';
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

ajax "/chart_data/monthly/:year" => sub {
    my $data = [[
        map [ timegm(0, 0, 0, 1, $_, 2014) * 1000, $Monthly->[$_][2] ],
            0 .. $#$Monthly
    # ],[
        # map [ timegm(0, 0, 0, 1, $_, 2014) * 1000, $Monthly->[$_][0] ],
            # 0 .. $#$Monthly
    ]];
    to_json($data)
};

my $Daily = [
    0,
    0,
    0,
    0,
    0,
    42,
    1083,
    833,
    708,
    583,
    167,
    167,
    208,
    167,
    42,
    83,
    42,
    42,
    0,
    0,
    0,
    0,
    0,
    0,
];

ajax "/chart_data/daily/:day" => sub {
    my $tot = 0; $tot += $_ for @$Daily;
    my $data = [[
        map [ timegm(0, 0, $_, 12, 1, 2015) * 1000, $Daily->[$_] / $tot * 100 ],
            0 .. $#$Daily
    ]];
    to_json($data)
};

true;
