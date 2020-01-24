import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Home extends Component {
  displayName = Home.name

  constructor(props) {
    super(props);
    this.state = { auctions: [], loading: true, bid: '', username: '' };
    this.handleInputChange = this.handleInputChange.bind(this)
    this.handleSubmit = this.handleSubmit.bind(this)

    fetch('/api/auction/live')
      .then(response => response.json())
      .then(data => {
        this.setState({ auctions: data, loading: false });
      });
  }

  handleInputChange(event) {
    const target = event.target
    const name = target.name

    this.setState({
      [name]: target.value
    })
  }

  handleSubmit(event) {
    const bid = {
      description: this.state.bid,
      username: this.state.username
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
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <table className='table'>
        <thead>
          <tr>
            <th>Description</th>
            <th>Starting Bid</th>
            <th>Highest Bid</th>
            <th>Ending</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {this.state.auctions.map(auction =>
            <tr key={auction.id}>
              <td>{auction.description}</td>
              <td>{auction.startingPrice}</td>
              <td>{auction.highestBid}</td>
              <td>{new Date(auction.endTime).toLocaleString()}</td>
              <td><Link to={{
                pathname: "/bid",
                state: {id: auction.id}
                }}
                ><button className="btn btn-primary">Bid</button></Link></td>

            </tr>
          )}
        </tbody>
      </table>;

    return (
      <div>
        <h1>Live Auctions</h1>
        {contents}
      </div>
    );
  }
}
