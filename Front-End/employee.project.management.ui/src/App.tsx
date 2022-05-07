import React, { Component } from "react";
import {BrowserRouter as Router, Route, Switch} from "react-router-dom";
import Home from './components/home/home';
import About from './components/about/about';
import Contact from './components/contact/contact';
import NoMatch from './components/noMatch/noMatch';
import Navigation from './components/navigation/navigation';
import Container from '@mui/material/Container';

class App extends Component {

	render() {
		return (
			<React.Fragment>
				<Router>				
					<div className="App">
						<Navigation/>
						<Container maxWidth="lg">
							<Switch>
								<Route exact path="/" component={Home}/>
								<Route path="/cart" component={Home}/>
								<Route path="/contact" component={Contact}/>
								<Route path="/about" component={About}/>
								<Route component={NoMatch}/>
							</Switch>
						</Container>
					</div>
				</Router>
       		</React.Fragment>
		);
	}
}

export default App;
