import React, { Component } from 'react'
import {Redirect} from 'react-router-dom';

export class Bid extends Component {
  displayName = Bid.name

  constructor(props) {
    console.log(props.location.state)
    super(props)
    this.state = { id: props.location.state.id, amount: '', redirect: false }

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
    console.log(this.state.id)
    const bid = {
      id: this.state.id,
      amount: parseFloat(this.state.amount)
    }
    const payload = JSON.stringify(bid)
    fetch('api/bid', {
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
        <h1>Bid on an item</h1>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="amount">Amount</label>
            <input
              onChange={this.handleInputChange}
              value={this.state.amount}
              type="number"
              className="form-control"
              id="amount"
              name="amount" />
          </div>
          <button onChange={this.handleInputChange} type="submit" className="btn btn-primary">Place bid</button>
        </form>
      </div>
    );
  }
}
