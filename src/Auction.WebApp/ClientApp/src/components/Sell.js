import React, { Component } from 'react'
import {Redirect} from 'react-router-dom';

export class Sell extends Component {
  displayName = Sell.name

  constructor(props) {
    super(props)
    this.state = { description: '', startingPrice: '', redirect: false }

    this.handleInputChange = this.handleInputChange.bind(this)
    this.handleSubmit = this.handleSubmit.bind(this)
  }

  handleInputChange(event) {
    const target = event.target
    const name = target.name

    this.setState({
      [name]: target.value
    })
  }

  handleSubmit(event) {
    const auction = {
      description: this.state.description,
      username: this.state.username,
      startingPrice: parseFloat(this.state.startingPrice)
    }
    const payload = JSON.stringify(auction)
    fetch('api/auction', {
      method: 'post',
      headers: {
        'Content-Type': 'application/json'
      },
      body: payload
    }).then(() => this.setState(() => ({
      redirect: true
    })));
    event.preventDefault()
  }

  render() {
    if (this.state.redirect === true) {
      return <Redirect to='/' />
    }
    return (
      <div>
        <h1>Sell an item</h1>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="description">Description</label>
            <input
              onChange={this.handleInputChange}
              value={this.state.description}
              type="text"
              className="form-control"
              id="description"
              name="description"
              placeholder="Enter description of item" />
          </div>
          <div className="form-group">
            <label htmlFor="startingPrice">Minimum Bid</label>
            <input
              onChange={this.handleInputChange}
              value={this.state.startingPrice}
              type="number"
              className="form-control"
              id="startingPrice"
              name="startingPrice"
              placeholder="Enter the minimum acceptable bid" />
          </div>
          <button onChange={this.handleInputChange} type="submit" className="btn btn-primary">Submit</button>
        </form>
      </div>
    );
  }
}
