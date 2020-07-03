import { store } from '@risingstack/react-easy-state';

const AuthProvider = store({
    isLoggedIn: false,
    JWTToken: "",
    ToggleIsLoggedIn() {
        AuthProvider.isLoggedIn = !AuthProvider.isLoggedIn;
    },
    SetToken() {

    }
});

export default AuthProvider;
