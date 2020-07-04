import React, { Component } from 'react';
import {
  BrowserRouter as Router,
  Switch,
  Route,
} from "react-router-dom";
import { view } from '@risingstack/react-easy-state';
import NavBar from './Components/NavBar';
import About from './Pages/About';
import Home from './Pages/Home';
import Metrics from './Pages/Metrics';
import WebSites from './Pages/Websites';
import Grid from '@material-ui/core/Grid';

class App extends Component {
  render() {
    return (
      <Router>
        <div className="App">
          <NavBar></NavBar>
          <div style={{ paddingTop: "20px" }}>
            <Grid container spacing={1}>
              {/* <Grid item xs={1}>
              </Grid> */}
              <Grid item xs={12}>
                <Switch>
                  <Route path="/about">
                    <About />
                  </Route>
                  <Route path="/websites">
                    <WebSites />
                  </Route>

                  <Route path="/metrics">
                    <Metrics />
                  </Route>
                  <Route path="/">
                    <Home />
                  </Route>
                </Switch>
              </Grid>
              {/* <Grid item xs={1}>

              </Grid> */}
            </Grid>
          </div>
        </div>
      </Router >
    )
  }

}
export default view(App)
