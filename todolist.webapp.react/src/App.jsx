import MainPage from './components/MainPage.jsx'
import LogInPage from './auth/LoginPage.jsx'
import { useAuth } from './components/AuthContext.jsx'
import { Register } from './auth/Register.jsx'

function App() {
    const { token } = useAuth();

    return (<>
        {token == null && <div>
            <LogInPage />
            <Register />
            </div>}
        { token && <MainPage /> }
        </>
    )
}

export default App
