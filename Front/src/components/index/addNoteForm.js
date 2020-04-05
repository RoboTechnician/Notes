import React from "react";

class AddNoteForm extends React.Component {
    constructor(props) {
        super(props);

        this.titleChange = this.titleChange.bind(this);
        this.textChange = this.textChange.bind(this);
        this.submit = this.submit.bind(this);

        this.state = { title: '', text: '', titleError: '', textError: '' };
    }

    titleChange(event) {
        this.setState({ title: event.target.value });
    }

    textChange(event) {
        this.setState({ text: event.target.value });
    }

    submit(event) {
        event.preventDefault();

        let error = false;

        return fetch('api/note/add', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                'Accept': 'application/json',
                "Authorization": "Bearer " + this.props.token
            },
            body: JSON.stringify({
                'Title': this.state.title,
                'Text': this.state.text
            })
        }).then(res => {
            if (res.ok) {
                this.setState({ title: '', text: '', titleError: '', textError: '' });
                this.props.getNotes();
            }
            else {
                error = true;
                if (res.status >= 500)
                    throw new Error("Internal server error");
                else
                    return res.json();
            }
        }).then(res => {
            if (error)
                this.setState({ titleError: res.title, textError: res.text });
        }).catch(err => this.setState({ textError: err }));
    }

    render() {
        return (
            <form onSubmit={this.submit}>
                <input className="form-control" value={this.state.title} type="text" placeholder="Title" onChange={this.titleChange} />
                <label className="error">{this.state.titleError}</label>
                <textarea className="form-control" value={this.state.text} type="password" placeholder="Text" onChange={this.textChange}></textarea>
                <label className="error">{this.state.textError}</label>
                <button className="btn btn-lg btn-primary btn-block mt-3" type="submit">Add</button>
            </form>
        );
    }
}

export default AddNoteForm;