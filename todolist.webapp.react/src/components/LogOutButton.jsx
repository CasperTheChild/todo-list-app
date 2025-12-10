import { useAuth } from './AuthContext.jsx'

export function LogOutButton() {
    const { logOut } = useAuth();

    return (
        <button onClick={() => logOut() }>
            LogOut
        </button>
    )
}