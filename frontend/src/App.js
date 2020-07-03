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
import WebSites from './Pages/Websites';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';
class App extends Component {
  render() {
    return (
      <Router>
        <div className="App">
          <NavBar></NavBar>
          <Container style={{ paddingTop: "20px" }}>
            <Grid container spacing={1}>
              <Grid item xs={1}>

              </Grid>
              <Grid item xs={10}>
                <Switch>
                  <Route path="/about">
                    <About />
                  </Route>
                  <Route path="/websites">
                    <WebSites />
                  </Route>
                  <Route path="/">
                    <Home />
                  </Route>
                </Switch>
              </Grid>
              <Grid item xs={1}>

              </Grid>
            </Grid>
          </Container>
        </div>
      </Router >
    )
  }

}
export default view(App)
