/** @jsx React.DOM */

var React = require('react');

var EndpointRow = React.createClass({
	render: function(){
		var hash = '#fubumvc/chain-details/' + this.props.endpoint.hash;
	
		return (
			<tr>
				<td><a href={hash}>Details</a></td>
				<td>{this.props.endpoint.title}</td>
				<td>{this.props.endpoint.actions}</td>
			</tr>
		);
	}
});

var EndpointTable = React.createClass({
	getInitialState: function(){
		return {
			loading: true
		}
	},

	componentDidMount: function(){
		FubuDiagnostics.get('EndpointExplorer:endpoints', {}, data => {
			_.assign(data, {loading: false});

			this.setState(data);
		});
	},

	render: function(){
		if (this.state.loading){
			return (<p>Loading...</p>);
		}

		var rows = this.state.endpoints.map(function(e, i){
			return (
				<EndpointRow endpoint={e} />
			);
		});
	
		return (
			<table className="table">
				<thead>
					<tr>
						<th>View Details</th>
						<th>Description</th>
						<th>Action(s)</th>
					</tr>
				</thead>
				<tbody>
					{rows}
				</tbody>
			</table>
		);
	}
});

FubuDiagnostics.section('fubumvc').add({
	title: 'Endpoints',
	description: 'All the configured endpoint routes, partials, and message handlers in this application',
	key: 'endpoints',
	component: EndpointTable
});