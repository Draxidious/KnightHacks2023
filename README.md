# What is this?

This is a project made for the KnightHacks 2023 hackathon, authored by Jordan, Jack, August, and Gabe. InterView is an immersive interview experience powered by a unique AI pipeline designed to assess your professionalism, charisma, and proficiency in this one-on-one conversation simulator. 

## Inspiration

Jack is a mentor at [First Step @ UCF](https://www.instagram.com/firststep.ucf/?hl=en) and leads his organization in succeeding in their early career. We were inspired by his mentees' desire to hone their networking and interview skills without having to physically meet with recruiters and hiring managers (hi sponsors!).

InterView was the perfect opportunity for us to learn how to integrate AI into our first generative project and serve individuals wanting to practice their interview skills.

## What it does

InterView places you in an immersive one-on-one with an interviewer powered by a unique instruction-tuned AI paradigm we conceived ourselves. An operator agent drives the conversation with our user while three supervisor agents assess the conversation using three criteria -- Professionalism, Charisma, and Proficiency. We utilize the Oculus Voice SDK to allow the user to speak to their interviewer and receive text-to-speech back to simulate the flow of verbal conversation, and complete an interview with a final breakdown of the user's performance. 

## How we built it

We created the VR environment in Unity and C#. August is passionate for making fun and inviting virtual spaces, and we think he really nailed it for InterView. We also implemented three components that we're especially stoked to have pulled off: 

We knew that we wanted our user to have the ability to speak to and hear our interviewer -- conversational tempo and tone is crucial for nailing an interview. Jordan found that the Oculus Voice SDK was perfect for this use-case, and made the implementation that allows for us to parse our user's dialogue into text for our agents to work off of.

Our second and third components are part of our AI pipeline. We had the idea that we wanted to quantify the quality of the conversation by criteria that would be impactful to measure for interview feedback. We realized that we could implement a creative solution: We implemented three separate supervisor agents that each gauge a different measurement for each piece of the conversation. We use these measurements to serve the user feedback on different aspects of their dialogue so that they can deliberately improve.

## Challenges we ran into

August: The biggest challenge I ran into was getting the physics right for the objects in the scene. There was a lot more to the collision and throw physics than I anticipated.

Gabe: I had a lot more trouble creating the UIs than I thought I might have. I've never used Unity before so it was considerably different from creating a UI in React.

Jordan: The text-to-speech and speech-to-text was not made to work in VR. [Laughs] and Meta used depricated objects to run their voice SDK. Their forums wer also not helpful. So I basically had to rever-seengineer the samples and then reengineer them to work in VR. 

Jack: Definitely the speech-to-text, it was a really hard thing. We also ran into a really weird git merge problem. It was also pretty surprising having our API key revoked by OpenAI for publishing it within our Unity project repo.

## Accomplishments that we're proud of

August: I'm really happy with how the interviewer turned out, I spent a lot of time creating the assets and animations that contribute to the character's personality!

Gabe: I'm really proud of the instruction-tuned AI agents that we were able to implement. We spent a lot of time tweaking the operator agent's conversation tendencies and the supervisors' rating scales.

Jordan: I'm really happy that we got the text-to-speech and speech-to-text working with the UI. It was an arduous handful of hours leading up to submission that was having us question whether we'd complete the project or not.

Jack: Using generative AI in Unity. I think doing anything with speech-to-text or text-to-speech was really difficult. I'm also really proud of August because our environment for this project turned out way better than our previous one.

## What we learned

August: I learned how to tweak facial animations and how to give a character personality.

Gabe: Leading up to the event I had spent the week learning Unity in preparation for a VR project, and I ended up learning more than double the amount during this hackathon.

Jordan: I learned that I never want to work with the OculusVoiceSDK again. 

Jack: I learned that Unity hates us [laughs] just generally about Unity itself and how annoying that some softwares are so deprecated that they're barely useful but still left in. I think it's pretty annoying.

## What's next for InterView - AI Powered Interview Prep

[What's next is To Be Decided]

![IMG_4338](https://github.com/Draxidious/KnightHacks2023/assets/86631042/26ecc48a-5ff9-4e35-b1dc-849c873a735d)
![IMG_4372](https://github.com/Draxidious/KnightHacks2023/assets/86631042/ca66c12b-ee7c-448d-af24-927563a248f9)
![IMG_4391](https://github.com/Draxidious/KnightHacks2023/assets/86631042/0e2c2437-50e6-4da0-a6b4-5b0c0bf48a59)
![IMG_4390](https://github.com/Draxidious/KnightHacks2023/assets/86631042/1dcedaf5-861c-4c71-9c76-c2654748ba91)
![IMG_4396](https://github.com/Draxidious/KnightHacks2023/assets/86631042/ebb5e6af-fa1f-40c2-8300-9181fd75bc33)

