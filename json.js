let j = {"root": [
    {"x": 10, "y": 20, }, 
    {"x": 30, "y": 40, },
]};

console.log (j);

let s = JSON.stringify (j);

console.log (s);

let j2 = JSON.parse (s);

console.log (j2);
