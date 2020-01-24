import React, { Component } from 'react';

export class Finished extends Component {
  displayName = Finished.name

  constructor(props) {
    super(props);
    this.state = { auctions: [], loading: true };

    fetch('/api/auction/finished')
      .then(response => response.json())
      .then(data => {
        this.setState({ auctions: data, loading: false });
      });
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : <table className='table'>
        <thead>
          <tr>
            <th>Description</th>
            <th>Winning Bid</th>
            <th>Ended</th>
          </tr>
        </thead>
        <tbody>
          {this.state.auctions.map(auction =>
            <tr key={auction.id}>
              <td>{auction.description}</td>
              <td>{auction.highestBid === 0.0 ? 'No bids' : auction.highestBid}</td>
              <td>{new Date(auction.endTime).toLocaleString()}</td>
            </tr>
          )}
        </tbody>
      </table>;

    return (
      <div>
        <h1>Finished Auctions</h1>
        {contents}
      </div>
    );
  }
}
