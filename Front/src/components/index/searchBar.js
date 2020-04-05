import React from "react";

class SearchBar extends React.Component {
    constructor(props) {
        super(props);

        this.searchChange = this.searchChange.bind(this);
        this.submit = this.submit.bind(this);

        this.state = { search: '' };
    }

    searchChange(event) {
        this.setState({ search: event.target.value });
    }

    submit(event) {
        event.preventDefault();
        this.props.getNotes(this.state.search);
    }

    render() {
        return (
            <form className="row" onSubmit={this.submit}>
                <input className="form-control col" type="text" placeholder="Search..." onChange={this.searchChange} />
                <button className="btn btn-primary" type="submit">Search</button>
            </form>
        );
    }
}

export default SearchBar;