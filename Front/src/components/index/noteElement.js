import React from "react";

class NoteElement extends React.Component {
    constructor(props) {
        super(props);
        this.delete = this.delete.bind(this);
    }

    delete(event) {
        event.preventDefault();
        this.props.deleteNote(this.props.note.id);
    }

    render() {
        return (
            <div className="col note">
                <div className="row note-top">
                    {this.props.note.title != '' &&
                        <p className="h5 col title"><strong>{this.props.note.title}</strong></p>
                    }
                    <button className="btn btn-delete" onClick={this.delete}>Delete</button>
                </div>
                <p className="text-content">{this.props.note.text}</p>
            </div>
        );
    }
}

export default NoteElement;