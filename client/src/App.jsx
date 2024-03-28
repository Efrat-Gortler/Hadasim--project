import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import MemberListPage from './component/MemberListPage' 



function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <MemberListPage></MemberListPage>
    </>
  )
}

export default App
