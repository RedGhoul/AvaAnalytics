import React, { Component } from "react";
import AuthProvider from '../Stores/AuthProvider';
import { view } from '@risingstack/react-easy-state';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import { withRouter } from "react-router-dom";
import { Link } from 'react-router-dom';
class NavBar extends Component {
    handleChange = (event, value) => {
        this.props.history.push(value);
    };
    render() {

        return (
            <AppBar position="static" color="default">
                <Toolbar>
                    <Typography style={{ flexGrow: 1 }} variant="h6" >
                        Sharp Counter
                    </Typography>
                    <Button component={Link}
                        to="/About"
                        label="signal"
                        value="signal" color="inherit">About</Button>
                    <Button component={Link}
                        to="/Websites"
                        label="signal"
                        value="signal" color="inherit">Websites</Button>
                    <Button onClick={AuthProvider.ToggleIsLoggedIn}
                        label="signal"
                        value="signal" color="inherit">Login</Button>
                </Toolbar>

            </AppBar >
        )
    }
}
export default withRouter(view(NavBar))
