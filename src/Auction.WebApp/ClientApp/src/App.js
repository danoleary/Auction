import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Sell } from './components/Sell';
import { Bid } from './components/Bid';
import { Finished } from './components/Finished';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/sell' component={Sell} />
        <Route path='/bid' component={Bid} />
        <Route path='/finished' component={Finished} />
      </Layout>
    );
  }
}
