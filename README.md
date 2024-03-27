# PatientMedicationAPI - Dovile Baranauskaite

Please have a read before running the application.

### How to run the app

To run the web app locally, open an IDE of your choice (I prefer Visual Studio) and start the app. Swagger page should launch shortly.

Then either call the APIs directly from the page or Postman.

There are also some unit tests to run.

### The APIs completed

POST - adds a new Medication request based on data provided. I expect the API user to know exact Patient, Clinician and Medication IDs there. Another way to implement it would be to do a lookup if only Clinician Name or Medication Code was present.

GET - gets a Medication request filtered by patient ID and optional filters of status and start / end dates.

### Improvements - what I wish I had done!

I have skipped the PATCH API considering I shouldn't spend more than 3 hours on the task. I have already spent around that time on it.
If I was to add PATCH API I would have followed the same pattern as for GET and POST requests. Taking in End Date, Frequency and Status, validating the values and applying database changes.

If I spent more time on the task I would have also written more granular unit tests and seen how I could possibly test the DB queries.

Another thing I considered when creating the database migration was Status (of MedicationRequest) and Form (of Medication). These fields have been saved as strings in the given DB tables but I think if I had time I would create separate DB tables just for them. Since there's only a small number of possible values it could be a nice way to separate them and avoid any DB entry errors in case validation did not work.

Docker - I actually didn't have time to look into this but I generated a Dockerfile when solution was created. 
