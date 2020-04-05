import React from "react";
import NoteElement from './noteElement';

class NotesList extends React.Component {
    render() {
        let notes = [];

        this.props.notes.forEach(note => {
            notes.push(<NoteElement note={note} deleteNote={this.props.deleteNote} />);
        })

        return (
            <div className="notes">
                {notes}
            </div>
        );
    }
}

export default NotesList;