import './App.css'
import { Box, Typography } from '@mui/material'
import TeamsSearchPage from './components/teams/Search/TeamSearch'

function App() {

  return (
    <>
     <Box sx={{width: 1000}}>
       <Typography variant="h1" >
        Football Teams
      </Typography>
      <TeamsSearchPage></TeamsSearchPage>
     </Box>
    </>
  )
}

export default App
