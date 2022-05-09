import React, { Component } from "react";
import {Routes, Route} from "react-router-dom";
import Home from './components/home/home';
import About from './components/about/about';
import Contact from './components/contact/contact';
import NoMatch from './components/noMatch/noMatch';
import Employee from './components/employee/employee';
import Project from './components/project/project';
import Navigation from './components/navigation/navigation';
import Container from '@mui/material/Container';

class App extends Component {

  render() {
    return (
      <>
          <Navigation/>
          <Container maxWidth="lg">
            <Routes>
                <Route path="/" element={<Home/>} />
                <Route path="employee" element={<Employee/>} />
                <Route path="project" element={<Project/>} />
                <Route path="contact" element={<Contact/>} />
                <Route path="about" element={<About/>} />
                <Route path="*" element={<NoMatch/>} />
            </Routes>
          </Container>          
      </>
    );
  }
}

export default App;
