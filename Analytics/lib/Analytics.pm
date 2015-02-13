package Analytics;
use Dancer ':syntax';
use Dancer::Plugin::Ajax;

our $VERSION = '0.1';

get '/' => sub {
    template 'index';
};

get '/analytics' => sub {
    template 'analytics';
};

ajax "/chart_data/:user_id/:measure_id" => sub {
    # no_cache;
    # debug "Returning chart data from [" . params->{user_id} . "]["
                                        # . params->{measure_id} . "]";
    # my $data = AM::User->chart_data(params->{user_id}, params->{measure_id});
    # my $data = [[ 10, 10, 1, "abc", 10, "ABC" ]];
    my $data = [[[ 10, 20 ], [ 20, 15 ], [ 30, 25 ]]];
    # Don't use Data::Dumper to print out $data - it will turn some numbers
    # into strings which to_json will quote and which will make jqplot unhappy.
    # Data::Printer is OK.
    # debug p $data;
    to_json($data)
};

true;
