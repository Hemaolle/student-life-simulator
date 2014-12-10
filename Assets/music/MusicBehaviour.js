#pragma strict

var startingSound : AudioClip;
var loopingSound : AudioClip;
 
function Start()
{
    audio.clip = startingSound;
    audio.Play();
    PlayQueued(loopingSound, true);
}
 
function PlayQueued(next : AudioClip, loop : boolean)
{
    yield WaitForSeconds(audio.clip.length - audio.time);
    audio.clip = next;
    audio.loop = loop;
    audio.Play();
}

function Update () {

}